﻿using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);

            if(product.ProductID > 0)
                _db.Products.Update(product);
            else
                _db.Products.Add(product);


            await _db.SaveChangesAsync();

            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productID)
        {
            try{
                Product product = await _db.Products?.FirstOrDefaultAsync(u => u.ProductID == productID);
           
                if (product == null)
                    return false;

                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productID)
        {
            Product product = await _db.Products.Where(x => x.ProductID == productID).FirstOrDefaultAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> productList = await _db.Products.ToListAsync();

            return _mapper.Map<List<ProductDto>>(productList);
        }
    }
}
