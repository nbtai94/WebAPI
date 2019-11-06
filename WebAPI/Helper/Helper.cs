using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Helper
{
    public class Helper
    {
        public static string ConvertToNormalize(string words)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            while ((index = words.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(words[index]);
                words = words.Replace(words[index], ReplText[index2]);
            }
            return words;
        }
        public static string GenerateCode(DateTime date,int element)
        {
            WebAPIContext db = new WebAPIContext();
            CodeManager manager = db.CodeManagers.Where(e => e.Element == element).SingleOrDefault();
            if (manager.ResetIndex == 1)
            {
                if (date.Day == 1)
                {
                    manager.Index = 1;
                    db.SaveChanges();
                }
            }
            else
            {
                if (date.Day == 1&&date.Month==1)
                {
                    manager.Index = 1;
                    db.SaveChanges();
                }
            }
            var zeroNumber = string.Empty;
            for (int i = 0; i < manager.NumberOfZeroInNumber - manager.Index.ToString().Length; i++)
            {
                zeroNumber += "0";
            }
            string Code = manager.Prefix + date.ToString(manager.DateFormat) + "/" + zeroNumber + manager.Index;
            manager.Index++;
            db.SaveChanges();
            return Code;
        }
    }
}

