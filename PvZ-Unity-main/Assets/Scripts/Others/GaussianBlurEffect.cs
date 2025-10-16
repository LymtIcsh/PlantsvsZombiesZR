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
        // ֻ�ܰ� pass enqueue����Ҫ���� cameraColorTargetHandle
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

        // �� Setup/CamSetup ���ȡ descriptor�����ڵ�һ������ʱ���� RTHandle
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // �õ���ǰ���������ȾĿ��������
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

            // **������** ��ȫ���õ� cameraColorTargetHandle
            RTHandle source = renderingData.cameraData.renderer.cameraColorTargetHandle;

            CommandBuffer cmd = CommandBufferPool.Get("Gaussian Blur");

            // 1) ��Դ��ȾĿ�� Blit ����ʱ������ģ��
            cmd.Blit(source, tempTexture, blurMaterial);
            // 2) �ٰ�ģ����� Blit ��ȥ
            cmd.Blit(tempTexture, source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            // ���ϣ��ÿ֡���ͷţ����������� cmd.ReleaseTemporaryRT(...) �� RTHandles.Release(tempTexture)
        }
    }
}
