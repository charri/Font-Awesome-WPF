using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FontAwesome.WPF.Generate.Annotations;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FontAwesome.WPF.Generate
{
    public class FontAwesomeInterop
    {
        private readonly ConfigContainer _config;
        private readonly IconContainer _iconContainer;

        public FontAwesomeInterop(string configYaml)
        {
            
            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention(), ignoreUnmatched: true);
            _config = deserializer.Deserialize<ConfigContainer>(new StreamReader(configYaml));
            
            if(string.IsNullOrEmpty(_config.IconMeta)) throw new Exception("icon meta");

            var iconPath = Path.Combine(Path.GetDirectoryName(configYaml), _config.IconMeta);

            if(!File.Exists(iconPath))
                throw new FileNotFoundException("icon.yaml file specified in _config.yaml could not be found", iconPath);

            _iconContainer = deserializer.Deserialize<IconContainer>(new StreamReader(iconPath));
        }

        public IEnumerable<IconEntry> Items
        {
            get { return _iconContainer.Icons; }
        }

        public FontAwesomeConfig Config
        {
            get { return _config.FontAwesome; }
        }

        public ConfigContainer Container
        {
            get { return _config; }
        }

        #region [ Deserialize ]
        [UsedImplicitly]
        public class ConfigContainer
        {
            [YamlAlias("icon_meta")]
            public string IconMeta { get; set; }

            [YamlAlias("icon_destination")]
            public string IconDestination { get; set; }

            [YamlAlias("fontawesome")]
            public FontAwesomeConfig FontAwesome { get; set; }
        }

        [UsedImplicitly]
        public class FontAwesomeConfig
        {
            [YamlAlias("doc_blob")]
            public string DocBlob { get; set; }

            public string Url { get; set; }

            public string Tagline { get; set; }

            public Author Author { get; set; }

            public Github Github { get; set; }
        }

        [UsedImplicitly]
        public class IconContainer
        {
            public List<IconEntry> Icons { get; set; } 
        }
        [UsedImplicitly]
        public class Author
        {
            public string Name { get; set; }

            public string Github { get; set; }
        }
        [UsedImplicitly]
        public class Github
        {
            public string Url { get; set; }
        }

        [UsedImplicitly]
        public class IconEntry
        {
            private static readonly Regex REG_PROP = new Regex(@"\([^)]*\)");

            public string Name { get; set; }
            public string Id { get; set; }
            public string Unicode { get; set; }
            public string Created { get; set; }

            public List<string> Aliases { get; set; }

            public List<string> Categories { get; set; }

            private string _safeName = null;

            [YamlIgnore]
            public string SafeName
            {
                get
                {
                    if (string.IsNullOrEmpty(_safeName))
                    {
                        _safeName = Safe(Id);
                    }
                    return _safeName;
                }
            }

            public string Safe(string text)
            {
                var cultureInfo = Thread.CurrentThread.CurrentCulture;
                var textInfo = cultureInfo.TextInfo;

                if (text.EndsWith("-o") || text.Contains("-o-"))
                    text = text.Replace("-o", "-outline");
                
                var stringBuilder = new StringBuilder(textInfo.ToTitleCase(text.Replace("-", " ")));

                stringBuilder
                    .Replace("-", string.Empty).Replace("/", "_")
                    .Replace(" ", string.Empty).Replace(".", string.Empty)
                    .Replace("'", string.Empty);

                var matches = REG_PROP.Matches(stringBuilder.ToString());
                stringBuilder = new StringBuilder(REG_PROP.Replace(stringBuilder.ToString(), string.Empty));
                var hasMatch = false;

                for (var i = 0; i < matches.Count; i++)
                {
                    var match = matches[i];
                    if (match.Value.IndexOf("Hand", StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        hasMatch = true;
                        break;
                    }
                }
                
                if (hasMatch)
                {
                    stringBuilder.Insert(0, "Hand");
                }

                if (char.IsDigit(stringBuilder[0]))
                    stringBuilder.Insert(0, '_');
                
                return stringBuilder.ToString();
            }
        }
        #endregion
    }
}
