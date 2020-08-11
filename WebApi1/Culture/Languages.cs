using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Culture
{
    public class Languages : List<Language>
    {
        #region 语言信息

        //zs     zh-cn      简体中文            简体中文            Simplified Chinese
        //en     en-us      English            英语                English
        //zt     zh-tw      繁体中文            繁体中文            Traditional Chinese
        //pt     pt-br      Português          葡萄牙语            Portuguese
        //es     es-es      Español            西班牙语            Spanish
        //fr     fr-fr      Français           法语                France
        //it     it-it      Italiano           意大利语            Italy
        //ko     ko-kr      한국어              韩语                Korean
        //ja     ja-jp      日本語              日本语              Japanese
        //de     de-de      Deutsch            德语                German
        //ru     ru-ru      Русский            俄语                Russian
        //ar     ar-ae     العربية            阿拉伯语            Arabic
        //bg     bg-bg      български          保加利亚语          Bulgarian
        //ca     ca         Català             加泰罗尼亚语         Catalan
        //cs     cs-cz      čeština            捷克语              Czech
        //da     DA-DK      dansk              丹麦语              Danish
        //nl     nl-nl      Nederlands         荷兰语              Dutch
        //et     et-ee      Eesti              爱沙尼亚语          Estonia
        //fi     fi-fi      suomi              芬兰语              Finnish
        //el     el-gr      Ελληνικά           希腊语              Greek
        //ht     ht         Kreyòl Ayisyen     海地克里奥尔语       Haitian Creole
        //he     he-il      עברית             希伯来语            Hebrew
        //hi     hi-in      हिंदी                印地文              Hindi
        //mww    mww        Hmoob dawb         白苗语              Hmong Daw
        //hu     hu-hu      magyar             匈牙利语            Hungarian
        //id     id-id      Indonesia          印尼语              Indonesia
        //lv     lv-lv      Latviešu           拉脱维亚语          Latvian
        //lt     lt-lt      Lietuvių           立陶宛语            Lithuanian
        //no     nb-no      Norsk              挪威语              Norwegian
        //fa     fa-ir      فارسی             波斯语              Persian
        //pl     pl-pl      Polski             波兰语              Polish
        //ro     ro-ro      Română             罗马尼亚语          Romanian
        //th     th-th      ภาษาไทย             泰语                Thai
        //tr     tr-tr      Türkçe             土耳其语            Turkish
        //uk     uk-ua      українська         乌克兰              Ukrainian
        //vi     vi-vn      Việt Nam           越南                Vietnam
        //sv     sv-se      Svenska            瑞典语              Swedish
        //sl     sl-si      slovenščina        斯洛文尼亚语         Slovenian
        //sk     sk-sk      slovenčina         斯洛伐克语          Slovak
        //mt     mt-mt      Malti              马耳他语            Maltese
        //lo     lo-la      ພາສາລາວ            老挝语              Laotian
        //km     km-kh      ភាសាខ្មែរ            高棉语              Khmer

        public static Language zs { get; } = new Language() { Code = "zs", Culture = "zh-cn", Native = "简体中文", Name = "简体中文", English = "SimplifiedChinese" };
        public static Language en { get; } = new Language() { Code = "en", Culture = "en-us", Native = "English", Name = "英语", English = "English" };
        public static Language zt { get; } = new Language() { Code = "zt", Culture = "zh-tw", Native = "繁体中文", Name = "繁体中文", English = "TraditionalChinese" };
        public static Language pt { get; } = new Language() { Code = "pt", Culture = "pt-br", Native = "Português", Name = "葡萄牙语", English = "Portuguese" };
        public static Language es { get; } = new Language() { Code = "es", Culture = "es-es", Native = "Español", Name = "西班牙语", English = "Spanish" };
        public static Language fr { get; } = new Language() { Code = "fr", Culture = "fr-fr", Native = "Français", Name = "法语", English = "France" };
        public static Language it { get; } = new Language() { Code = "it", Culture = "it-it", Native = "Italiano", Name = "意大利语", English = "Italy" };
        public static Language ko { get; } = new Language() { Code = "ko", Culture = "ko-kr", Native = "한국어", Name = "韩语", English = "Korean" };
        public static Language ja { get; } = new Language() { Code = "ja", Culture = "ja-jp", Native = "日本語", Name = "日本语", English = "Japanese" };
        public static Language de { get; } = new Language() { Code = "de", Culture = "de-de", Native = "Deutsch", Name = "德语", English = "German" };
        public static Language ru { get; } = new Language() { Code = "ru", Culture = "ru-ru", Native = "Русский", Name = "俄语", English = "Russian" };
        public static Language ar { get; } = new Language() { Code = "ar", Culture = "ar-ae", Native = "العربية", Name = "阿拉伯语", English = "Arabic" };
        public static Language bg { get; } = new Language() { Code = "bg", Culture = "bg-bg", Native = "български", Name = "保加利亚语", English = "Bulgarian" };
        public static Language ca { get; } = new Language() { Code = "ca", Culture = "ca", Native = "Català", Name = "加泰罗尼亚语", English = "Catalan" };
        public static Language cs { get; } = new Language() { Code = "cs", Culture = "cs-cz", Native = "čeština", Name = "捷克语", English = "Czech" };
        public static Language da { get; } = new Language() { Code = "da", Culture = "DA-DK", Native = "dansk", Name = "丹麦语", English = "Danish" };
        public static Language nl { get; } = new Language() { Code = "nl", Culture = "nl-nl", Native = "Nederlands", Name = "荷兰语", English = "Dutch" };
        public static Language et { get; } = new Language() { Code = "et", Culture = "et-ee", Native = "Eesti", Name = "爱沙尼亚语", English = "Estonia" };
        public static Language fi { get; } = new Language() { Code = "fi", Culture = "fi-fi", Native = "suomi", Name = "芬兰语", English = "Finnish" };
        public static Language el { get; } = new Language() { Code = "el", Culture = "el-gr", Native = "Ελληνικά", Name = "希腊语", English = "Greek" };
        public static Language ht { get; } = new Language() { Code = "ht", Culture = "ht", Native = "KreyòlAyisyen", Name = "海地克里奥尔语", English = "HaitianCreole" };
        public static Language he { get; } = new Language() { Code = "he", Culture = "he-il", Native = "עברית", Name = "希伯来语", English = "Hebrew" };
        public static Language hi { get; } = new Language() { Code = "hi", Culture = "hi-in", Native = "हिंदी", Name = "印地文", English = "Hindi" };
        public static Language mw { get; } = new Language() { Code = "mww", Culture = "mww", Native = "Hmoobdawb", Name = "白苗语", English = "HmongDaw" };
        public static Language hu { get; } = new Language() { Code = "hu", Culture = "hu-hu", Native = "magyar", Name = "匈牙利语", English = "Hungarian" };
        public static Language id { get; } = new Language() { Code = "id", Culture = "id-id", Native = "Indonesia", Name = "印尼语", English = "Indonesia" };
        public static Language lv { get; } = new Language() { Code = "lv", Culture = "lv-lv", Native = "Latviešu", Name = "拉脱维亚语", English = "Latvian" };
        public static Language lt { get; } = new Language() { Code = "lt", Culture = "lt-lt", Native = "Lietuvių", Name = "立陶宛语", English = "Lithuanian" };
        public static Language no { get; } = new Language() { Code = "no", Culture = "nb-no", Native = "Norsk", Name = "挪威语", English = "Norwegian" };
        public static Language fa { get; } = new Language() { Code = "fa", Culture = "fa-ir", Native = "فارسی", Name = "波斯语", English = "Persian" };
        public static Language pl { get; } = new Language() { Code = "pl", Culture = "pl-pl", Native = "Polski", Name = "波兰语", English = "Polish" };
        public static Language ro { get; } = new Language() { Code = "ro", Culture = "ro-ro", Native = "Română", Name = "罗马尼亚语", English = "Romanian" };
        public static Language th { get; } = new Language() { Code = "th", Culture = "th-th", Native = "ภาษาไทย", Name = "泰语", English = "Thai" };
        public static Language tr { get; } = new Language() { Code = "tr", Culture = "tr-tr", Native = "Türkçe", Name = "土耳其语", English = "Turkish" };
        public static Language uk { get; } = new Language() { Code = "uk", Culture = "uk-ua", Native = "українська", Name = "乌克兰", English = "Ukrainian" };
        public static Language vi { get; } = new Language() { Code = "vi", Culture = "vi-vn", Native = "ViệtNam", Name = "越南", English = "Vietnam" };
        public static Language sv { get; } = new Language() { Code = "sv", Culture = "sv-se", Native = "Svenska", Name = "瑞典语", English = "Swedish" };
        public static Language sl { get; } = new Language() { Code = "sl", Culture = "sl-si", Native = "slovenščina", Name = "斯洛文尼亚语", English = "Slovenian" };
        public static Language sk { get; } = new Language() { Code = "sk", Culture = "sk-sk", Native = "slovenčina", Name = "斯洛伐克语", English = "Slovak" };
        public static Language mt { get; } = new Language() { Code = "mt", Culture = "mt-mt", Native = "Malti", Name = "马耳他语", English = "Maltese" };
        public static Language lo { get; } = new Language() { Code = "lo", Culture = "lo-la", Native = "ພາສາລາວ", Name = "老挝语", English = "Laotian" };
        public static Language km { get; } = new Language() { Code = "km", Culture = "km-kh", Native = "ភាសាខ្មែរ", Name = "高棉语", English = "Khmer" };

        /// <summary>
        /// 语言列表
        /// </summary>
        public static Languages Source { get; } = new Languages()
        {
            zs,
            en,
            zt,
            pt,
            es,
            fr,
            it,
            ko,
            ja,
            de,
            ru,
            ar,
            bg,
            ca,
            cs,
            da,
            nl,
            et,
            fi,
            el,
            ht,
            he,
            hi,
            mw,
            hu,
            id,
            lv,
            lt,
            no,
            fa,
            pl,
            ro,
            th,
            tr,
            uk,
            vi,
            sv,
            sl,
            sk,
            mt,
            lo,
            km
        };

        #endregion

    }
}