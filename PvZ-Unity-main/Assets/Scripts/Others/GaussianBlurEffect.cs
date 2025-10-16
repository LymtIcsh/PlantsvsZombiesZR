using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GaussianBlurEffect : ScriptableRendererFeature
{
    [System.Serializable]
    public class BlurSettings
    {
        public Material blurMaterial;
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public BlurSettings settings = new BlurSettings();
    BlurPass blurPass;

    public override void Create()
    {
        blurPass = new BlurPass(settings);
        blurPass.renderPassEvent = settings.renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // 只管把 pass enqueue，不要访问 cameraColorTargetHandle
        renderer.EnqueuePass(blurPass);
    }

    class BlurPass : ScriptableRenderPass
    {
        Material blurMaterial;
        RTHandle tempTexture;
        RenderTextureDescriptor descriptor;
        bool allocated;

        public BlurPass(BlurSettings settings)
        {
            blurMaterial = settings.blurMaterial;
        }

        // 在 Setup/CamSetup 里获取 descriptor，并在第一次运行时分配 RTHandle
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // 拿到当前摄像机的渲染目标描述符
            descriptor = renderingData.cameraData.cameraTargetDescriptor;
            descriptor.depthBufferBits = 0;

            if (!allocated)
            {
                tempTexture = RTHandles.Alloc(
                    descriptor,
                    filterMode: FilterMode.Bilinear,
                    wrapMode: TextureWrapMode.Clamp,
                    name: "_TempBlurTexture"
                );
                allocated = true;
            }
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (blurMaterial == null)
                return;

            // **在这里** 安全地拿到 cameraColorTargetHandle
            RTHandle source = renderingData.cameraData.renderer.cameraColorTargetHandle;

            CommandBuffer cmd = CommandBufferPool.Get("Gaussian Blur");

            // 1) 把源渲染目标 Blit 到临时纹理并做模糊
            cmd.Blit(source, tempTexture, blurMaterial);
            // 2) 再把模糊结果 Blit 回去
            cmd.Blit(tempTexture, source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            // 如果希望每帧都释放，可以在这里 cmd.ReleaseTemporaryRT(...) 或 RTHandles.Release(tempTexture)
        }
    }
}
