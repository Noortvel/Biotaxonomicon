using Biotaxonomicon.StringResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.StringResources
{
    public class StringResources
    {
        public static StringResources Instance
        {
            get;
            set;
        }
        public string BioTaxonomicon = "BioTaxonomicon";
        public string Game = "Интерактивное дерево";
        public string Cabinet = "Кабинет";
        public string SuccesCreated = "Успешно созданно";
        public string ErrorCreate = "Ошибка создания";
        public string DataError = "Ошибка данных";
        public string TaxNodeParentWithTagDontExist = "Родительского узла с такой меткой не существует";
        public TextFieldErrors Login;
        public TextFieldErrors Nick;
        public TextFieldErrors Password;
    }
}
