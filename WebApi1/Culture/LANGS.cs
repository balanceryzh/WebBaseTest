using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Culture
{
    public static class LANGExtentions
    {
        /// <summary>
        /// 获取语言
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static Language GetLanguage(this string lang)
        {
            lang = !string.IsNullOrWhiteSpace(lang) ? lang.ToLower() : "";
            return Languages.Source.Where(x => x.Code == lang.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// 获取区域编码(国际标准编码zh-CN,en-US.....)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetCulture(this string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return code;
            switch (code.ToLower())
            {
                case "zs":
                    return "zh-CN";
                case "en":
                    return "en-US";
                case "zt":
                    return "zh-tw";
                case "pt":
                    return "pt-pt";
                case "es":
                    return "es-es";
                case "fr":
                    return "fr-fr";
                case "it":
                    return "it-it";
                case "ko":
                    return "ko-kr";
                case "ja":
                    return "ja-jp";
                case "de":
                    return "de-de";
                case "ru":
                    return "ru-ru";
                case "ar":
                    return "ar-ae";
            }
            return code;
        }

        /// <summary>
        /// 获取语言Code(zs,en,......)
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string GetLangCode(this string culture)
        {
            if (string.IsNullOrWhiteSpace(culture))
                return culture;

            culture = culture.ToLower();

            var langFormat = string.Format("|{0}|", culture);

            if ("|zs|zh-cn|zh-sg|zh-chs|zh|".Contains(langFormat))
                return "zs";

            if ("|en|en-us|en-gb|en-au|en-bz|en-ca|en-cb|en-ie|en-jm|en-nz|en-ph|en-za|en-tt|en-zw|".Contains(langFormat))
                return "en";

            if ("|zh-tw|zh-hk|zh-mo|zh-cht|".Contains(langFormat))
                return "zt";

            if ("|pt|pt-br|pt-pt|".Contains(langFormat))
                return "pt";

            if ("|es|es-es|es|es-ar|es-bo|es-cl|es-co|es-cr|es-do|es-ec|es-sv|es-gt|es-hn|es-mx|es-ni|es-pa|es-py|es-pe|es-pr|es-es|es-uy|es-ve|".Contains(langFormat))
                return "es";

            if ("|fr|fr-fr|fr-be|fr-ca|fr-lu|fr-mc|fr-ch|".Contains(langFormat))
                return "fr";

            if ("|it|it-it|it-ch|".Contains(langFormat))
                return "it";

            if ("|ko|ko-kr|".Contains(langFormat))
                return "ko";

            if ("|ja|ja-jp|".Contains(langFormat))
                return "ja";

            if ("|de|de-de|de-at|de-li|de-lu|de-ch|".Contains(langFormat))
                return "de";

            if ("|ru|ru-ru|".Contains(langFormat))
                return "ru";

            if ("|ar|ar-ae|ar-dz|ar-bh|ar-eg|ar-iq|ar-jo|ar-kw|ar-lb|ar-ly|ar-ma|ar-qa|ar-sa|ar-ye|".Contains(langFormat))
                return "ar";

            if ("|bg|bg-bg|".Contains(langFormat))
                return "bg";

            if ("|ca|".Contains(langFormat))
                return "bg";

            if ("|cs|cs-cz|".Contains(langFormat))
                return "cs";

            if ("|DA|DA-DK|".Contains(langFormat))
                return "da";

            if ("|nl|nl-nl|nl-be|".Contains(langFormat))
                return "nl";

            if ("|et|et-ee|".Contains(langFormat))
                return "nl";

            if ("|fi|fi-fi|".Contains(langFormat))
                return "fi";

            if ("|el|el-gr|".Contains(langFormat))
                return "el";

            if ("|ht|".Contains(langFormat))
                return "ht";

            if ("|he|he-il|".Contains(langFormat))
                return "he";

            if ("|hi|hi-in|".Contains(langFormat))
                return "hi";

            if ("|mww|".Contains(langFormat))
                return "mww";

            if ("|hu|hu-hu|".Contains(langFormat))
                return "hu";

            if ("|id|id-id|".Contains(langFormat))
                return "hu";

            if ("|lv|lv-lv|".Contains(langFormat))
                return "lv";

            if ("|lt|lt-lt|".Contains(langFormat))
                return "lt";

            if ("|no|nb-no|nn-no|".Contains(langFormat))
                return "no";

            if ("|fa|fa-ir|".Contains(langFormat))
                return "fa";

            if ("|pl|pl-pl|".Contains(langFormat))
                return "pl";

            if ("|ro|ro-ro|".Contains(langFormat))
                return "ro";

            if ("|th|th-th|".Contains(langFormat))
                return "th";

            if ("|tr|tr-tr|".Contains(langFormat))
                return "tr";

            if ("|uk|uk-ua|".Contains(langFormat))
                return "uk";

            if ("|vi|vi-vn|".Contains(langFormat))
                return "vi";

            if ("|sv|sv-se|sv-fi|".Contains(langFormat))
                return "sv";

            if ("|sl|sl-si|".Contains(langFormat))
                return "sl";

            if ("|sk-sk|sk|".Contains(langFormat))
                return "sk";

            if ("|mt|mt-mt|".Contains(langFormat))
                return "mt";

            if ("|lo|lo-la|".Contains(langFormat))
                return "lo";

            if ("|km|km-kh|".Contains(langFormat))
                return "km";

            return culture;
        }


        /// <summary>
        /// 提示：xxxxx成功
        /// </summary>
        /// <returns></returns>
        public static string SuccessTip(this string str)
        {
            return $"{str}{LANG.ChengGong}";
        }

        /// <summary>
        /// 提示：xxxxx失败
        /// </summary>
        /// <returns></returns>
        public static string FailTip(this string str)
        {
            return $"{str}{LANG.ShiBai}";
        }

        /// <summary>
        /// 提示：xxxxx错误
        /// </summary>
        /// <returns></returns>
        public static string ErrorTip(this string str)
        {
            return $"{str}{LANG.CuoWu}";
        }

        /// <summary>
        /// 提示：xxxxx创建失败
        /// </summary>
        /// <returns></returns>
        public static string CreateFailTip(this string str)
        {
            return $"{str}创建失败";
        }


        /// <summary>
        /// 提示：XXX不能为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CheckNullEmptyTip(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return LANG.BuNengWeiKong;
            return $"{str}{LANG.BuNengWeiKong}";
        }


        /// <summary>
        /// 提示：未找到XXX
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NotFoundTip(this string str)
        {
            return string.Format(LANG.WeiZhaoDaoFormat, !string.IsNullOrWhiteSpace(str) ? str : LANG.XinXi);
        }

        /// <summary>
        /// 提示：XXX已存在
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IsExistTip(this string str)
        {
            return string.Format(LANG.YiCunZaiFormat, !string.IsNullOrWhiteSpace(str) ? str : LANG.XinXi);
        }

        /// <summary>
        /// 提示：请输入xxxxx
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string InputTip(this string str)
        {
            return $"{LANG.QingShuRu}{str}";
        }


        /// <summary>
        /// 提示：xxx管理
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ManageTip(this string str)
        {
            return $"{str}{LANG.GuanLi}";
        }

        /// <summary>
        /// 提示：允许xxxxx
        /// </summary>
        /// <returns></returns>
        public static string YunXuTip(this string str)
        {
            return $"{LANG.YunXu}{str}";
        }


    }
}