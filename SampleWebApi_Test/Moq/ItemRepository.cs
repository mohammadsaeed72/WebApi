using SampleWebApi._1.Entities;
using SampleWebApi._2.Database.Repositories;
using SampleWebApi._4.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi_Test.Moq
{
    public class ItemRepositoryMoq : IItemRepository
    {
        private List<Item> _items = new List<Item>();
        public string _itemId = Guid.NewGuid().ToString();
        public ItemRepositoryMoq()
        {
            for (int i = 0; i < 10; i++)
            {
                _items.Add(new Item()
                {
                    Code = $"Code{i}",
                    Description = $"Desc{i}",
                    Id = _itemId +i,
                    Group = new ItemGroup()
                    {
                        Id = i,
                        Description = $"desc{i}",
                        InsertedDate = DateTime.Now,
                        Name = $"GroupName{i}",
                    },
                    LstDiscounts = new List<ItemDiscount> { new ItemDiscount() { Id=i,Discount = 0.05M+i/10, FromDate = DateTime.Now, ToDate = DateTime.Now.AddDays(30), InsertedDate = DateTime.Now, Name = $"تخفیف شماره {i}" } },
                    LstItemPrice = new List<ItemPrice> { new ItemPrice() { Id = i, Price = 250000+(i*1500), InsertedDate = DateTime.Now, FromDate = DateTime.Now } },
                    InsertedDate = DateTime.Now,
                    LstFeatures = new List<ItemFeature> { new ItemFeature { Id=i,Feature = new Feature() {Id=i, InsertedDate = DateTime.Now, Name = "رنگ" }, InsertedDate = DateTime.Now, Value = "red" } },
                    Name = $"کالای شماره {i}",
                    IsActive = true,
                    ShortDescription = $"short Diesc {i}",
                    LstItemTransactions = new List<ItemTransaction> {
                        new ItemTransaction()
                        { InsertedDate=DateTime.Now,
                            InvoiceLine=new InvoiceLine() { InvoiceId=$"lksdhfsdhsdlfjhasdfklagsdfasdju{i}",InsertedDate=DateTime.Now,Amount=100,DiscountPerUnit=0,PricePerUnit=230000+(i*1500),InvoiceType=InvoiceType.Buy},
                    } ,
                        new ItemTransaction()
                        { InsertedDate=DateTime.Now,
                            InvoiceLine=new InvoiceLine() { InvoiceId=$"lksdhfsdhsdlfjhasdfklagsdfasdju{i}",InsertedDate=DateTime.Now.AddHours(5),Amount=20,DiscountPerUnit=0.05M,PricePerUnit=250000+(i*1500),InvoiceType=InvoiceType.Sale},
                    } ,
                        new ItemTransaction()
                        { InsertedDate=DateTime.Now,
                            InvoiceLine=new InvoiceLine() {InvoiceId=$"lksdhfsdhsdlfjhasdfklagsdfasdju{i}", InsertedDate=DateTime.Now.AddHours(36),Amount=5,DiscountPerUnit=0.05M,PricePerUnit=250000+(i*1500),InvoiceType=InvoiceType.ReturnSale},
                    } ,
                    },
                });
            }

        }
        public async Task<Item> AddAsync(Item item)
        {
            item.Id = _itemId+ _items.Count;
            _items.Add(item);
            return item;
        }

        public async Task<Item> GetByIdAsync(string id)
        {
           var item=  _items.FirstOrDefault(a => a.Id == id);
            return item;
        }

        public async Task<List<Item>> GetOrderedByDateAsync(int top)
        {
            var lst = _items.Take(top).ToList();
            return lst;
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            var i = _items.FirstOrDefault(a => a.Id == item.Id);
            _items.Remove(item);
            _items.Add(item);
            return item;
        }

        public async Task<List<SimilarItemViewModel>> GetItemListByPriceRangeAsync(decimal minPrice, decimal maxPrice, int count)
        {
           var result= _items
                .Where(a => a.GetPrice(DateTime.Now).Price >= minPrice && a.GetPrice(DateTime.Now).Price <= maxPrice)
                .OrderByDescending(a => a.InsertedDate)
                .Take(count)
                .Select(a=>new SimilarItemViewModel()
                {
                    Code= a.Code,
                    Id= a.Id,
                    Name= a.Name,
                    ShortDescription= a.ShortDescription,
                    Price=a.LstItemPrice.OrderByDescending(a=>a.InsertedDate).FirstOrDefault().Price
                }).ToList();

            return result;
        }
    }
}
