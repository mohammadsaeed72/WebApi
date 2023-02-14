using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SampleWebApi._1.Entities;
using SampleWebApi._1.Entities.Base;
using SampleWebApi._2.Database.Repositories.Base;
using SampleWebApi._4.ViewModels.Items;
using System.Data;
using System.Threading;

namespace SampleWebApi._2.Database.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {

        public ItemRepository(AppDbContext dbContext,IConfiguration configuration):base(configuration,dbContext)
        {
        }


        public async Task<List<Item>> GetOrderedByDateAsync(int top)
        {
            var lstItem = await TableSet
                .Include(a => a.ItemGroupId)
                .Include(a => a.LstDiscounts)
                .Include(a => a.LstItemTransactions)
                .Include(a => a.LstFeatures).ThenInclude(a => a.Feature)
                .Include(a => a.LstItemPrice)
                .OrderByDescending(a => a.InsertedDate)
                .Take(top)
                .ToListAsync();

            return lstItem;
        }
        public async Task<List<Item>> GetOrderedByDateAsync(int page,int pageSize)
        {
            var lstItem = await TableSet
                .Include(a => a.ItemGroupId)
                .Include(a => a.LstDiscounts)
                .Include(a => a.LstItemTransactions)
                .Include(a => a.LstFeatures).ThenInclude(a => a.Feature)
                .Include(a => a.LstItemPrice)
                .OrderByDescending(a => a.InsertedDate)
                .Skip((page-1)*pageSize).Take(pageSize)
                .ToListAsync();

            return lstItem;
        }

        public async Task<Item> AddAsync(Item item)
        {
            await TableSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            TableSet.Update(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<Item> GetByIdAsync(string id)
        {
            var item = await TableSet.Where(a => a.Id == id)
                 .Include(a => a.ItemGroupId)
                 .Include(a => a.LstDiscounts)
                 .Include(a => a.LstItemTransactions)
                 .Include(a => a.LstFeatures).ThenInclude(a => a.Feature)
                 .Include(a => a.LstItemPrice).FirstOrDefaultAsync();

            return item;
        }

        public async Task<List<SimilarItemViewModel>> GetItemListByPriceRangeAsync(decimal minPrice,decimal maxPrice,int count)
        {
            using var connection = new SqlConnection(ConnectionString);
            string query = @"Select top(@count) itm.Id,itm.Name,itm.Code,itm.ShortDescription,prc.Price From Item as itm
                                Inner Join ItemPrice as prc on itm.Id=prc.ItemId and GetDate() between prc.FromDate And ISNULL(prc.ToDate,'2090-01-01')
                                Where prc.Price Between @minPrice And @maxPrice
                                order by itm.InsertedDate Desc";

            
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@minPrice", minPrice);
            queryParameters.Add("@maxPrice", maxPrice);
            queryParameters.Add("@count", count);


            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            var result= await connection.QueryAsync<SimilarItemViewModel>(query, queryParameters,commandType:CommandType.Text);

            return result.ToList();
        }



    }
}
