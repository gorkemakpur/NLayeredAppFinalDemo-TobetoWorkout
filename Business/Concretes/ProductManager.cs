using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest)
        {
            Product product = new Product();
            product.Id = Guid.NewGuid();
            product.ProductName = createProductRequest.ProductName;
            product.QuantityPerUnit = createProductRequest.QuantityPerUnit;
            product.UnitPrice = createProductRequest.UnitPrice;
            product.UnitsInStock = createProductRequest.UnitsInStock;

            Product createdProduct = await _productDal.AddAsync(product);
            CreatedProductResponse createdProductResponse = new CreatedProductResponse();
            createdProductResponse.Id = Guid.NewGuid();
            createdProductResponse.ProductName = createProductRequest.ProductName;
            createdProductResponse.QuantityPerUnit = createProductRequest.QuantityPerUnit;
            createdProductResponse.UnitPrice = createProductRequest.UnitPrice;
            product.UnitsInStock = createProductRequest.UnitsInStock;

            return createdProductResponse;
        }

        public async Task<IPaginate<GetListedProductResponse>> GetListAsync()
        {
            //paginate içinde product listesi
            var result = _productDal.GetListAsync();
            
            //getListedProductResponse 
            List<GetListedProductResponse>getList = new List<GetListedProductResponse>();

            //product list mapping
            foreach (var item in result.Result.Items)
            {
                GetListedProductResponse getListedProductResponse = new GetListedProductResponse();
                getListedProductResponse.Id = item.Id;
                getListedProductResponse.ProductName = item.ProductName;
                getListedProductResponse.UnitPrice=item.UnitPrice;
                getListedProductResponse.QuantityPerUnit=item.QuantityPerUnit;
                getListedProductResponse.UnitsInStock = item.UnitsInStock;
                getList.Add(getListedProductResponse);
            }
            //paginate mapping
            Paginate<GetListedProductResponse> _paginate = new Paginate<GetListedProductResponse>();
            _paginate.Pages = result.Result.Pages;
            _paginate.Items = getList;
            _paginate.Index = result.Result.Index;
            _paginate.Size = result.Result.Size;
            _paginate.Count = result.Result.Count;
            _paginate.From = result.Result.From;
            //_paginate.HasNext=result.Result.HasNext; //auto value
            //_paginate.HasPrevious = result.Result.HasPrevious; //auto value
            return _paginate;
        }
    }
}
