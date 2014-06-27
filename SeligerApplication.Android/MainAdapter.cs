using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SeligerApplication.Android
{

    public class Sample
    {
        public Sample(string name, string smena, int img)
        {
            Name = name;
            Smena = smena;
            Img = img;
        }
        public string Name;
        public string Smena;
        public int Img;
    }

    class Header
    {
        public string Name;
        public int SectionIndex;
    }


    public class MainAdapter : BaseAdapter<Sample>
    {

        static Dictionary<string, List<Sample>> items = new Dictionary<string, List<Sample>>(){

            //Информация про первую смену "13 июля - 20 июля"
            {    "Форум молодежных проектов 1", new List<Sample>() {
                new Sample ("Беги за мной", "смена", Resource.Drawable.l1begizamnoy ),
                new Sample ("Команда 2018", "смена", Resource.Drawable.l1komanda2018),
                new Sample ("Технологии добра", "смена", Resource.Drawable.l1tehnologiyadobra),
                new Sample ("АРТ Квадрат", "смена", Resource.Drawable.l1artkvadrat),
            }
            },

            //вторая смена "20 июля - 27 июля",
            {
                "Форум молодежных проектов 2",  new List<Sample>() {
                new Sample ("Информационный поток", "смена", Resource.Drawable.l2infopotok),
                new Sample ("Молодежное самоуправление", "cмена", Resource.Drawable.l2molodejnoesamoupravlenie),
                new Sample ("Предпринимательство", "смена",  Resource.Drawable.l2predprinimatelstvo),
                //new Sample ("", "смена",  Resource.Drawable),
                new Sample ("Все дома", "смена",  Resource.Drawable.l2vsedoma),
                new Sample ("Инновации", "смена",  Resource.Drawable.l2innovacii)
            }
            },

            //третья смена "27 июля - 3 августа",
            {   "Форум \"Россия в центре\"",  new List<Sample>() {
                new Sample ("Студенческие организации", "смена",  Resource.Drawable.l3studencheskieorganizacii),
                new Sample ("Молодые дизайнеры и архитекторы", "смена",  Resource.Drawable.l3molodiedizaineri),
                new Sample ("Молодые юристы России", "смена",  Resource.Drawable.l3molodieuristi),
                new Sample ("Экономика будущего", "смена",  Resource.Drawable.l3ekonomikabuduchego),
                new Sample ("Молодые экологи", "смена",  Resource.Drawable.l3ecomol),
                new Sample ("Молодежный туризм", "смена",  Resource.Drawable.l3molodejniiturizm),
                new Sample ("Регионы России", "смена",  Resource.Drawable.l3regionirossii),
                new Sample ("Международная смена", "смена",  Resource.Drawable.l3selogerinternational)
            }
            },

            //четвёртая смена "3 августа - 10 августа",
            {   "Гражданский форум",  new List<Sample>() {
                new Sample ("Библиотекарь будущего", "смена",  Resource.Drawable.l4bibliotekabuduchego),
                new Sample ("Учитель будущего", "смена",  Resource.Drawable.l4uchitelbuduchego),
                new Sample ("Общественные объединения", "смена",  Resource.Drawable.l4vdvigenii),
                new Sample ("Военно-патриотические клубы", "смена",  Resource.Drawable.l4patrioticheskieklubi),
                new Sample ("Казачья молодежь", "смена",  Resource.Drawable.l4kazachiamolodej),
                new Sample ("Русская правда", "смена",  Resource.Drawable.l4russkayapravda),
                new Sample ("Духовные основы России", "смена",  Resource.Drawable.l4duhovnaiarossia),
                new Sample ("Работающая молодежь", "смена",  Resource.Drawable.l4rabotauchayamolodej)
                }
            }
            };

        const int TypeSectionHeader = 0;
        const int TypeSectionSample = 1;

        Activity context;

        readonly IList<object> rows = new List<object>();

        readonly ArrayAdapter<string> headers;
        readonly Dictionary<string, IAdapter> sections = new Dictionary<string, IAdapter>();

        public MainAdapter(Activity context)
        {
            this.context = context;
            headers = new ArrayAdapter<string>(context, Resource.Layout.MainHeaders, Resource.Id.Text1);

            rows = new List<object>();
            foreach (var section in items.Keys) {
                headers.Add(section);
                sections.Add(section, new ArrayAdapter<Sample>(context, Android.Resource.Layout.SimpleListItem1, items [section]));
                rows.Add(new Header { Name = section, SectionIndex = sections.Count-1});
                foreach (var session in items[section]) {
                    rows.Add(session);
                }
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for this position
            var item = this.rows[position];

            View view;

            if (item is Header)
            {
                view = headers.GetView(((Header)item).SectionIndex, convertView, parent);
                view.Clickable = false;
                view.LongClickable = false;
                return view;
            }

            int i = position - 1;
            while (i > 0 && rows[i] is Sample)
                i--;
            Header h = (Header)rows[i];
            view = sections[h.Name].GetView(position, convertView, parent);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = ((Sample)item).Name;
            return view;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return rows.Count; }
        }

        public override Sample this[int position]
        {
            get
            { // this'll break if called with a 'header' position
                return (Sample)rows[position];
            }
        }

        public override int ViewTypeCount
        {
            get
            {
                return 1 + sections.Values.Sum(adapter => adapter.ViewTypeCount);
            }
        }

        public override int GetItemViewType(int position)
        {
            return rows[position] is Header
                ? TypeSectionHeader
                : TypeSectionSample;
        }
        public Sample GetSample(int position)
        {
            return (Sample)rows[position];
        }
    }

}