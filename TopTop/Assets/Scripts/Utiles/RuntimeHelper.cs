using UnityEngine;

namespace TopTop.Utiles
{
    public class RuntimeHelper
    {
        public static string getLanguage()
        {
            return Application.systemLanguage.ToString();
        }

        public static string selectStringByLanguage(string tr, string ing)
        {
            if(getLanguage() == Constants.TURKISH)
            {
                return tr;
            }
            else if(getLanguage() == Constants.ENGLISH)
            {
                return ing;
            }
            return ing;
        }
    }
}
