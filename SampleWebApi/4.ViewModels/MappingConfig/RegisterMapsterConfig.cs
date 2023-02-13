using Mapster;
using SampleWebApi._1.Entities;
using SampleWebApi._4.ViewModels.Items;
using System;

namespace SampleWebApi._4.ViewModels.MappingConfig
{
    public static class MapsterConfig
    {
        public static GetItemViewModel GetViewModel(this Item item,DateTime dateTime)
        {
            GetItemViewModel viewModel = item.Adapt<GetItemViewModel>();
            viewModel.Price = item.GetPrice(dateTime).Price;
            viewModel.DiscountPercent = item.GetDiscountPercent(dateTime);
            viewModel.DiscountValue = item.GetDiscountToman(dateTime);
            viewModel.Inventory = item.GetItemAmount(dateTime);
            viewModel.LstFeatures = item.LstFeatures.Adapt<List<ItemFeatureViewModel>>();

            return viewModel;
        }
    }
}
