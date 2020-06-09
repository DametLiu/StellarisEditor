using StellarisEditor.mod.data;
using StellarisEditor.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace StellarisEditor.pdx.scriptobject
{
    /**
     * Mod 配置文件实体类
     * 
     */
    public class PdxMod : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        public PdxMod()
        {
            _Tags.AddLast(new PdxModTag("Balance", "平衡性", false));
            _Tags.AddLast(new PdxModTag("Buildings", "建筑物", false));
            _Tags.AddLast(new PdxModTag("Loading Screen", "加载界面", false));
            _Tags.AddLast(new PdxModTag("Military", "陆军", false));
            _Tags.AddLast(new PdxModTag("Overhaul", "大型修改", false));
            _Tags.AddLast(new PdxModTag("Sound", "音效", false));
            _Tags.AddLast(new PdxModTag("Spaceships", "海军", false));
            _Tags.AddLast(new PdxModTag("Species", "种族", false));
            _Tags.AddLast(new PdxModTag("Technologies", "科技", false));
            _Tags.AddLast(new PdxModTag("Total Conversion", "全面改动", false));
            _Tags.AddLast(new PdxModTag("Diplomacy", "外交", false));
            _Tags.AddLast(new PdxModTag("Economy", "经济", false));
            _Tags.AddLast(new PdxModTag("Events", "事件", false));
            _Tags.AddLast(new PdxModTag("Fixes", "错误修复", false));
            _Tags.AddLast(new PdxModTag("Font", "字体", false));
            _Tags.AddLast(new PdxModTag("Galaxy Generation", "银河系生成", false));
            _Tags.AddLast(new PdxModTag("Gameplay", "游戏性", false));
            _Tags.AddLast(new PdxModTag("Graphics", "图形", false));
            _Tags.AddLast(new PdxModTag("Leaders", "领袖", false));
            _Tags.AddLast(new PdxModTag("Utilities", "工具", false));
            _Tags.AddLast(new PdxModTag("Translation", "翻译", false));
        }

        // Mod 名称
        private String _Name = "";
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        // MOD 封面图
        private String _Picture = "";
        public String Picture
        {
            get
            {
                return _Picture;
            }
            set
            {
                _Picture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Picture"));
            }
        }

        // MOD 版本号
        private String _Version = "";
        public String Version
        {
            get
            {
                return _Version;
            }
            set
            {
                _Version = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Version"));
            }
        }

        // 需要群星支持版本号
        private String _SupportedVersion = "";
        public String SupportedVersion
        {
            get
            {
                return _SupportedVersion;
            }
            set
            {
                _SupportedVersion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SupportedVersion"));
            }
        }

        // MOD 项目路径
        private String _Path = "";
        public String Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path"));
            }
        }

        // MOD 项目文件夹
        public String Directory
        {
            get
            {
                if (String.IsNullOrEmpty(Path))
                    return "";
                
                return Path.Substring(Path.LastIndexOf('/')+1);
            }
            set
            {
                var s = (ModGlobalData.MOD_PATH_ROOT + value + @"\").Replace(@"\", "/");
                Path = s.Substring(0, s.Length - 1);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Directory"));
            }
        }

        // Steam 上传mod以后生成的id
        private String _RemoteFileId = "";
        public String RemoteFileId
        {
            get
            {
                return _RemoteFileId;
            }
            set
            {
                _RemoteFileId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RemoteFileId"));
            }
        }

        // Mod 的分类标签集合
        private LinkedList<PdxModTag> _Tags = new LinkedList<PdxModTag>();
        public LinkedList<PdxModTag> Tags
        {
            get
            {
                return _Tags;
            }
            set
            {
                foreach (var p in value)
                    foreach (var t in _Tags)
                        if (t.Loclization_English.Equals(p.Loclization_English))
                            t.IsChecked = p.IsChecked;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tags"));
            }
        }

        //以选中的标签文本，用在界面显示上
        public String SelectedTags
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var c in _Tags)
                    if (c.IsChecked)
                        sb.Append(c.Locliaztion_Chinese).Append(',');
                return String.IsNullOrEmpty(sb.ToString()) ? "" : sb.ToString().Substring(0, sb.Length - 1);
            }
        }

        // Mod 项目使用得图片
        public BitmapImage BitmapImage
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Picture))
                    return BitmapUtil.BitmapToBitmapImage(Properties.Resources.placeholder, 150, 150);
                else if (Picture.Equals("thumbnail.png"))
                    return BitmapUtil.LoadImage(Path + "\\" + Picture, 150, 150);
                else
                    return BitmapUtil.LoadImage(Picture, 150, 150);
            }
        }

        public override string ToString()
        {

            return $"name=\"{Name}\"\n" +
                (String.IsNullOrEmpty(Picture) ? "" : $"picture=\"thumbnail.png\"\n") +
                $"version=\"{Version}\"\n" +
                $"supported_version=\"{SupportedVersion}\"\n" +
                (String.IsNullOrEmpty(Directory) ? ""  : $"path=\"{(ModGlobalData.MOD_PATH_ROOT + Directory).Replace(@"\", "/")}\"\n") +
                $"tags=\n{Tags.ToSting()}\n" +
                (String.IsNullOrEmpty(RemoteFileId) ? "" : $"remote_file_id={RemoteFileId}");
        }

        public PdxMod Clone()
        {
            var mod = new PdxMod()
            {
                Name = Name,
                Picture = Picture,
                Version = Version,
                SupportedVersion = SupportedVersion,
                Directory = Directory,
                Path = Path,
                RemoteFileId = RemoteFileId,
                IsModData = IsModData,
            };
            foreach (var a in Tags)
                foreach (var b in mod.Tags)
                    if (a.Loclization_English == b.Loclization_English)
                        b.IsChecked = a.IsChecked;

            return mod;
        }
    }

    public static class LinkedListExtension
    {
        public static String ToSting(this LinkedList<PdxModTag> list)
        {
            String str = "{\n";
            foreach (var tag in list)
                if (tag.IsChecked)
                    str += "\t" + tag.ToString() + "\n";
            str += "}";
            return str;
        }
    }
}
