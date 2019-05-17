using System;
using System.IO;
using Xunit;
using SkiaSharp;
using Microcharts;

namespace Test
{
    public class MainTest
    {
        public static readonly SKColor[] DefinedColors =
		{
			SKColor.Parse("#266489"),
			SKColor.Parse("#68B9C0"),
			SKColor.Parse("#90D585"),
			SKColor.Parse("#F3C151"),
			SKColor.Parse("#F37F64"),
			SKColor.Parse("#424856"),
			SKColor.Parse("#8F97A4"),
			SKColor.Parse("#DAC096"),
			SKColor.Parse("#76846E"),
			SKColor.Parse("#DABFAF"),
			SKColor.Parse("#A65B69"),
			SKColor.Parse("#97A69D"),
		};


        [Fact]
        public void TestBarChart()
        {
            var entries = new[]
            {
                new Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#266489")
                },
                new Entry(400)
                {
                    Label = "February",
                    ValueLabel = "400",
                    Color = SKColor.Parse("#68B9C0")
                },
                new Entry(600)
                {
                    Label = "March",
                    ValueLabel = "600",
                    Color = SKColor.Parse("#90D585")
                }
            };

            var chart = new BarChart();
            chart.Entries = entries;
            chart.ValueLabelStyle = ValueLabelSyle.VerticalDisplay;

            var dir = Path.Combine(AppContext.BaseDirectory, "Data");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var width = 500;
            var height = 300;

            using (var bitmap = new SKBitmap(width, height))
            using (var canvas = new SKCanvas(bitmap))
            {
                chart.Draw(canvas, width, height);
                var pngImage = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100);

                using (var fs = File.Create(Path.Combine(dir, Guid.NewGuid().ToString() + ".png")))
                {
                    pngImage.SaveTo(fs);
                    fs.Flush();
                    fs.Close();
                }
            }
        }
    }
}
