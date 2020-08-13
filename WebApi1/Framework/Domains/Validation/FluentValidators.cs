using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WebApi1.Culture;
using WebApi1.Engine;
using WebApi1.EnumBase;
using WebApi1.Resource;
using WebApi1.Utility;

namespace WebApi1.Framework
{
    /// <summary>
    /// 验证信息对象解析实现
    /// </summary>
    public class FluentValidatorDefaultFactory : ValidatorFactoryBase, IValidatorFactory
    {
        /// <summary>
        /// ctor
        /// </summary>
        public FluentValidatorDefaultFactory() : base() { }

        /// <summary>
        /// 创建IValidator对象
        /// </summary>
        /// <param name="validatorType"></param>
        /// <returns></returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            return EngineHelper.Resolve<IValidator>(validatorType);
        }
    }

    /// <summary>
    /// 验证提示
    /// </summary>
    public class FluentValidatorLanguageManager : FluentValidation.Resources.LanguageManager
    {
        /// <summary>
        /// ctor
        /// </summary>
        public FluentValidatorLanguageManager()
        {
            #region 规则列表(示例-中文)

            //Translate<EmailValidator>("'{PropertyName}' 不是有效的电子邮件地址。");
            //Translate<GreaterThanOrEqualValidator>("'{PropertyName}' 必须大于或等于 '{ComparisonValue}'。");
            //Translate<GreaterThanValidator>("'{PropertyName}' 必须大于 '{ComparisonValue}'。");
            //Translate<LengthValidator>("'{PropertyName}' 的长度必须在 {MinLength} 到 {MaxLength} 字符，您已经输入了 {TotalLength} 字符。");
            //Translate<MinimumLengthValidator>("\"{PropertyName}\"必须大于或等于{MinLength}个字符。您输入了{TotalLength}个字符。");
            //Translate<MaximumLengthValidator>("\"{PropertyName}\"必须小于或等于{MaxLength}个字符。您输入了{TotalLength}个字符。");
            //Translate<LessThanOrEqualValidator>("'{PropertyName}' 必须小于或等于 '{ComparisonValue}'。");
            //Translate<LessThanValidator>("'{PropertyName}' 必须小于 '{ComparisonValue}'。");
            //Translate<NotEmptyValidator>("请填写 '{PropertyName}'。");
            //Translate<NotEqualValidator>("'{PropertyName}' 不能和 '{ComparisonValue}' 相等。");
            //Translate<NotNullValidator>("请填写 '{PropertyName}'。");
            //Translate<PredicateValidator>("指定的条件不符合 '{PropertyName}'。");
            //Translate<AsyncPredicateValidator>("指定的条件不符合 '{PropertyName}'。");
            //Translate<RegularExpressionValidator>("'{PropertyName}' 的格式不正确。");
            //Translate<EqualValidator>("'{PropertyName}' 应该和 '{ComparisonValue}' 相等。");
            //Translate<ExactLengthValidator>("'{PropertyName}' 必须是 {MaxLength} 个字符，您已经输入了 {TotalLength} 字符。");
            //Translate<InclusiveBetweenValidator>("'{PropertyName}' 必须在 {From} 和 {To} 之间， 您输入了 {Value}。");
            //Translate<ExclusiveBetweenValidator>("'{PropertyName}' 必须在 {From} 和 {To} 之外， 您输入了 {Value}。");
            //Translate<CreditCardValidator>("'{PropertyName}' 不是有效的信用卡号。");
            //Translate<ScalePrecisionValidator>("'{PropertyName}' 总位数不能超过 {expectedPrecision} 位，其中整数部分 {expectedScale} 位。您填写了 {digits} 位小数和 {actualScale} 位整数。");
            //Translate<EmptyValidator>("\"{PropertyName}\"应该是空的。");
            //Translate<NullValidator>("\"{PropertyName}\"必须为空。");
            //Translate<EnumValidator>("\"{PropertyName}\"的值范围不包含\"{PropertyValue}\"。");

            #endregion

            #region 规则列表(示例-英文)

            //Translate<EmailValidator>("'{PropertyName}' is not a valid email address.");
            //Translate<GreaterThanOrEqualValidator>("'{PropertyName}' must be greater than or equal to '{ComparisonValue}'.");
            //Translate<GreaterThanValidator>("'{PropertyName}' must be greater than '{ComparisonValue}'.");
            //Translate<LengthValidator>("'{PropertyName}' must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters.");
            //Translate<MinimumLengthValidator>("The length of '{PropertyName}' must be at least {MinLength} characters. You entered {TotalLength} characters.");
            //Translate<MaximumLengthValidator>("The length of '{PropertyName}' must {MaxLength} characters or fewer. You entered {TotalLength} characters.");
            //Translate<LessThanOrEqualValidator>("'{PropertyName}' must be less than or equal to '{ComparisonValue}'.");
            //Translate<LessThanValidator>("'{PropertyName}' must be less than '{ComparisonValue}'.");
            //Translate<NotEmptyValidator>("'{PropertyName}' should not be empty.");
            //Translate<NotEqualValidator>("'{PropertyName}' should not be equal to '{ComparisonValue}'.");
            //Translate<NotNullValidator>("'{PropertyName}' must not be empty.");
            //Translate<PredicateValidator>("The specified condition was not met for '{PropertyName}'.");
            //Translate<AsyncPredicateValidator>("The specified condition was not met for '{PropertyName}'.");
            //Translate<RegularExpressionValidator>("'{PropertyName}' is not in the correct format.");
            //Translate<EqualValidator>("'{PropertyName}' should be equal to '{ComparisonValue}'.");
            //Translate<ExactLengthValidator>("'{PropertyName}' must be {MaxLength} characters in length. You entered {TotalLength} characters.");
            //Translate<InclusiveBetweenValidator>("'{PropertyName}' must be between {From} and {To}. You entered {Value}.");
            //Translate<ExclusiveBetweenValidator>("'{PropertyName}' must be between {From} and {To} (exclusive). You entered {Value}.");
            //Translate<CreditCardValidator>("'{PropertyName}' is not a valid credit card number.");
            //Translate<ScalePrecisionValidator>("'{PropertyName}' may not be more than {expectedPrecision} digits in total, with allowance for {expectedScale} decimals. {digits} digits and {actualScale} decimals were found.");
            //Translate<EmptyValidator>("'{PropertyName}' should be empty.");
            //Translate<NullValidator>("'{PropertyName}' must be empty.");
            //Translate<EnumValidator>("'{PropertyName}' has a range of values which does not include '{PropertyValue}'.");

            #endregion

            var zh = new CultureInfo("zh-CN");
            var en = new CultureInfo("en");

            AddTranslation("zh-CN", "NotNullValidator", LANG.ResourceManager.GetString("QingShuRuFormat", zh));
            AddTranslation("en", "NotNullValidator", LANG.ResourceManager.GetString("QingShuRuFormat", en));

            AddTranslation("zh-CN", "NotEmptyValidator", LANG.ResourceManager.GetString("QingShuRuFormat", zh));
            AddTranslation("en", "NotEmptyValidator", LANG.ResourceManager.GetString("QingShuRuFormat", en));

            AddTranslation("zh-CN", "LengthValidator", LANG.ResourceManager.GetString("ChangDuQuJianFormat", zh));
            AddTranslation("en", "LengthValidator", LANG.ResourceManager.GetString("ChangDuQuJianFormat", en));

            AddTranslation("zh-CN", "EmailValidator", LANG.ResourceManager.GetString("QingShuRuYouXiaoFormat", zh));
            AddTranslation("en", "EmailValidator", LANG.ResourceManager.GetString("QingShuRuYouXiaoFormat", en));

            AddTranslation("zh-CN", "MobileValidator", LANG.ResourceManager.GetString("QingShuRuYouXiaoFormat", zh));
            AddTranslation("en", "MobileValidator", LANG.ResourceManager.GetString("QingShuRuYouXiaoFormat", en));
        }
    }

    /// <summary>
    /// FluentValidator 扩展
    /// </summary>
    public static class FluentValidatorExtenstion
    {
        /// <summary>
        /// 验证Input
        /// </summary>
        /// <param name="input"></param>
        /// <param name="validator"></param>
        /// <param name="isException"></param>
        /// <returns></returns>
        public static IList<ValidationFailure> Verify<T>(this IInput input, T validator = default(T), bool isException = true)
            where T : IValidator, new()
        {
            //bool success = results.IsValid;
            //IList<ValidationFailure> failures = results.Errors;

            input.CheckNull("domain -> validator -> Verify : input");
            if (validator.IsNull())
            {
                validator = new T();
            }

            ValidationResult results = validator.Validate(input);
            if (!results.IsValid && results.Errors.Count > 0 && isException)
            {
                throw new CodeException(EnumCode.提示, results.Errors[0].ErrorMessage);
            }

            return results.IsValid ? new List<ValidationFailure>() : results.Errors;
        }

        /// <summary>
        /// 判断是否为手机号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> Mobile<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new MobileValidator());
        }
    }
}