using SkiaSharp;

namespace YoutubeInWebView.UI.Controls.SegmentedTimeline.Models
{
    public class TimelineCanvasConfig
    {
        public static readonly float SeparatorWidth = 10f;
        
        public static readonly SKPaint BackgroundPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#FFFFFF"),
            IsAntialias = true,
        };
        
        public static readonly SKPaint RightPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#A0A0A2"),
            IsAntialias = true,
        };
        
        public static readonly SKPaint SliderPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#FFDF8D"),
            IsAntialias = true,
        };
        
        public static readonly SKPaint BlackPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#0F0F1C"),
            IsAntialias = true,
        };

        public static SKPaint GetLeftPaint(float width, float height) =>
            new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0.5F * height),
                    new SKPoint(width, 0.5F * height),
                    new SKColor[] {SKColor.Parse("#28c3ba"), SKColor.Parse("#9662ec")},
                    new float[] {0, 1},
                    SKShaderTileMode.Clamp),
                IsAntialias = true,
            };
    }
}
