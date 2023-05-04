using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Project.Repository
{
    public class LocalRepository
    {
        static private Dictionary<int, Item> _items = null;

        //static internal void RetrieveCards()
        //{
        //if (_cardTypes == null) {
        //    var assembly = Assembly.GetExecutingAssembly();
        //    using (var reader = new StreamReader(assembly.GetManifestResourceStream("EX01_HearthStone.Resources.DataFiles.cardtypes.json")))
        //        _cardTypes = JsonConvert.DeserializeObject<List<CardType>>(reader.ReadToEnd());
        //}
        //}
        //static public List<CardType> GetCardTypes()
        //{
        //    RetrieveCards();
        //    return _cardTypes;
        //}

        //static public CardType GetCardType(int id)
        //{
        //    RetrieveCards();
        //    return _cardTypes.Find(card => card.Id == id);
        //}

        static public Dictionary<int, Item> GetItems()
        {
            if (_items == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var reader = new StreamReader(assembly.GetManifestResourceStream("Project.Resources.sampleData.json")))
                {
                    _items = JObject.Parse(reader.ReadToEnd()).SelectToken("items").Children().OfType<JProperty>().ToDictionary(x => int.Parse(x.Name), x => x.Value.ToObject<Item>());
                }
            }
            return _items;
        }
    }
}
