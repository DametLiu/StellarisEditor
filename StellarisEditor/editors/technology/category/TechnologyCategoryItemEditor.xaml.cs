﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Pfim;
using Pfim.dds;
using StellarisEditor.pdx.scriptobject;

namespace StellarisEditor.editors.technology.category
{
    /// <summary>
    /// TechnologyCategoryItemEditor.xaml 的交互逻辑
    /// </summary>
    public partial class TechnologyCategoryItemEditor : Window
    {
        private static List<GCHandle> handles = new List<GCHandle>();
        public TechnologyCategory TechnologyCategory { get; set; }

        public TechnologyCategoryItemEditor(TechnologyCategory technologyCategory)
        {
            InitializeComponent();

            TechnologyCategory = technologyCategory;
            DataContext = TechnologyCategory;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Images  (*.tga;*.dds)|*.tga;*.dds|All files (*.*)|*.*",
                Title = "Open Files with Pfim",
            };

            if (dialog.ShowDialog(this) != true)
                return;
            ImagePanel.Children.Clear();

            foreach (var handle in handles)
                handle.Free();

            var images = dialog.FileNames;
            filepath.Text = images[0];
            foreach (var file in images)
            {
                IImage image = await Task.Run(() => Pfim.Pfim.FromFile(file));
                foreach (var im in WpfImage(image))
                {
                    ImagePanel.Children.Add(im);
                }
            }
        }

        private static IEnumerable<Image> WpfImage(IImage image)
        {
            var pinnedArray = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
            var addr = pinnedArray.AddrOfPinnedObject();
            var bsource = BitmapSource.Create(image.Width, image.Height, 96.0, 96.0,
                PixelFormat(image), null, addr, image.DataLen, image.Stride);

            handles.Add(pinnedArray);
            yield return new Image
            {
                Source = bsource,
                Width = image.Width,
                Height = image.Height,
                MaxHeight = image.Height,
                MaxWidth = image.Width,
                Margin = new Thickness(4)
            };

            foreach (var mip in image.MipMaps)
            {
                var mipAddr = addr + mip.DataOffset;
                var mipSource = BitmapSource.Create(mip.Width, mip.Height, 96.0, 96.0,
                    PixelFormat(image), null, mipAddr, mip.DataLen, mip.Stride);
                yield return new Image
                {
                    Source = mipSource,
                    Width = mip.Width,
                    Height = mip.Height,
                    MaxHeight = mip.Height,
                    MaxWidth = mip.Width,
                    Margin = new Thickness(4)
                };
            }
        }

        private static PixelFormat PixelFormat(IImage image)
        {
            switch (image.Format)
            {
                case ImageFormat.Rgb24:
                    return PixelFormats.Bgr24;
                case ImageFormat.Rgba32:
                    return PixelFormats.Bgr32;
                case ImageFormat.Rgb8:
                    return PixelFormats.Gray8;
                case ImageFormat.R5g5b5a1:
                case ImageFormat.R5g5b5:
                    return PixelFormats.Bgr555;
                case ImageFormat.R5g6b5:
                    return PixelFormats.Bgr565;
                default:
                    throw new Exception($"Unable to convert {image.Format} to WPF PixelFormat");
            }
        }
    }
}
