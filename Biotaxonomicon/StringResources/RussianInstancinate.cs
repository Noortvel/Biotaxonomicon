using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.StringResources
{
    public static class RussianInstancinate
    {
        public static StringResources GetInstance()
        {
            var resources = new StringResources();
            resources.Login = StandartTemplate("Логин");
            resources.Nick = StandartTemplate("Псевдоним");
            resources.Password = StandartTemplate("Пароль");

            return resources;
        }
        private static TextFieldErrors StandartTemplate(string field)
        {
            var errors = new TextFieldErrors();
            errors.AlredyExist = $"{field} уже существует";
            errors.Invalid = $"{field} не валиден";
            errors.TooLong = $"{field} слишком длинный. Нужно больше 6 символов";
            errors.TooShort = $"{field} слишком короткий. Нужно больше 6 символов";
            return errors;
        }
    }
}
