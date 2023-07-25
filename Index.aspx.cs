using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Words;
using Aspose.Words.MailMerging;
using Newtonsoft.Json;

namespace WebApplication16
{
    public partial class WebForm : System.Web.UI.Page
    {
        public static string name1;
        public static string para;

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            // Get the user input from the text input field
            string name = txtName.Value.Trim();
            name1 = name;
            int n = (name.Length) % 4 ;
            // Load the template Word document
            Document doc = new Document(Server.MapPath("~/doc/mypdf.docx"));

            DocumentBuilder builder = new DocumentBuilder(doc);
            /*para = "";
            switch (n)
            {
                case 0:
                    para = "You will discover a hidden talent for dancing while doing laundry.Your washing machine hums along in perfect sync with your newly found dance prowess. The laundry detergent bottle transforms into your dance partner, and together, you perform the most breathtaking laundry room pas de deux ever witnessed.Your family and friends gather outside the door, captivated by your impromptu laundry ballet, which goes viral on social media and inspires millions to find joy in the most mundane tasks.";
                    break;
                case 1:
                    para = "You will become the world champion in Hand wrestling. News of your uncanny talent spreads like wildfire, and thumb wrestling enthusiasts from all corners of the globe gather to witness your prowess.Soon, you find yourself in an epic world championship showdown.Thumb wrestlers from various countries assemble to challenge you, but it's no match for your extraordinary digit dexterity. With each victory, you earn the adoration of millions and the title of the undisputed thumb wrestling champion.";
                    break;
                case 2:
                    para = "You will find your true love in a Amenity store while reaching for the last piece of chocolate.It's just an ordinary day at the grocery store, and you're browsing the aisles in search of a sweet treat.As you reach for the last piece of chocolate on the shelf, your hand brushes against another eager chocolate lover's hand. Your eyes meet, and in that moment, time seems to stand still.";
                    break;
                case 3:
                    para = "You will accidentally dead and become a famous internet meme for your hilarious cooking fails.One fateful day, you decide to showcase your culinary skills by attempting a seemingly simple recipe.However, as soon as you set foot in the kitchen, a series of comical mishaps ensue.Flour flies everywhere, vegetables tumble onto the floor, and pots and pans seem to conspire against you.Unbeknownst to you, a family member captures your culinary misadventures on camera.The video goes viral on social media, and before you know it";
                    break;
                default:
                    para = "podaa Loosu payale ";
                    break;
            }*/

            //para = "According to Numerology Name Numbers are very important in relationships with people, because the sound effects of your name produce certain patterns and expectations. The psychic number based on date is more important in close relationships, while the destiny number based on full date of birth";

            string jsonFilePath = @"C:\Users\Admin\source\repos\WebApplication16\doc\jsonfile.json";

            // Read the JSON file content
            string jsonContent = File.ReadAllText(jsonFilePath);

            List<FunnyContent> funnyContents = JsonConvert.DeserializeObject<List<FunnyContent>>(jsonContent);

            n++;
            FunnyContent foundContent = funnyContents.Find(cont => cont.Id == n);

            para = foundContent.content;

            var mergeData = new MergeData { Name = name ,Para = para};
            doc.MailMerge.FieldMergingCallback = new HandleMergeField(mergeData);
            doc.MailMerge.Execute(new string[] { "name" }, new object[] { name });

            
            // Save the output document with the replaced content
            doc.Save(Server.MapPath("~/GeneratedDocument.docx"));

            // Optionally, you can provide a download link to the generated document
            Response.Redirect("GeneratedDocument.docx");
        }
    }

    public class FunnyContent
    {
        public int Id { get; set; }
        public string content { get; set; }
    }


    public class MergeData
    {
        public string Name { get; set; }
        public string Para { get; set; }
    }


    public class HandleMergeField : IFieldMergingCallback
    {

        private readonly MergeData _mergeData;

        public HandleMergeField(MergeData mergeData)
        {
            _mergeData = mergeData;
        }


        public void FieldMerging(FieldMergingArgs args)
        {
            if (args.FieldName == "company_name")
            {
                args.Text = WebForm.name1;
            }
            
            if (args.FieldName == "Total")
            {
                args.Text = WebForm.para;
            }
        }
        public void ImageFieldMerging(ImageFieldMergingArgs args)
        {
            // We do not need to implement anything here.
        }
    }
}
