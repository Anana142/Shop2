using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
//using System.Text.Json;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;
using System.Linq;
using System.Threading.Tasks;

namespace Shop
{
    public class DataBase
    {
        List<Product> products = new List<Product>()
        {
            new Product()
            {   
                Id = 1,
                Title = "chair",
                Price = 2500,
                PathImage="Images/Sofa.png",
                IdCategory = 1,
                Description = "Discription"
                
            }
            
        };
        List<Category> categories = new List<Category>()
        {
            new Category()
            {
                Id = 1, 
                Title = "Chair"
            },
            new Category()
            {
                Id = 2,
                Title = "Sofa"
            }

        };

        string json;
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string fileName = "FunShopProductsList.json";
        string fileName2 = "FunShopCategory.json";

        public async Task<List<Product>> GetProductList()
        {
          json = File.ReadAllText(Path.Combine(folderPath, fileName));
          List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json); 
            return products; 
        }
        public async Task<List<Category>> GetCategoryList()
        {
            
            json = File.ReadAllText(Path.Combine(folderPath2, fileName2));
            categories = JsonConvert.DeserializeObject<List<Category>>(json);

            return categories;
        }

        public async void EditProduct(Product product, Category SelectedCategory)
        {
           
                products = GetProductList().Result;

                Product edit = products.FirstOrDefault(s => s.Id == product.Id);

                edit.Id = product.Id;
                edit.Title = product.Title;
                edit.Description = product.Description;
                edit.Category = SelectedCategory;
                edit.Price = product.Price;
                edit.PathImage = product.PathImage; 

                json = JsonConvert.SerializeObject(products);

                if (String.IsNullOrEmpty(fileName))
                    return;

                if (File.Exists(Path.Combine(folderPath, fileName)))
                {
                    File.WriteAllText(Path.Combine(folderPath, fileName), json);
                }
                else
                {
                    File.Create(Path.Combine(folderPath, fileName));
                }
            
        }
        public async void EditCategory(Category category) 
        {
            if (category.Title != null && category.Title != "")
            {
                categories = GetCategoryList().Result;
                Category edit = categories.FirstOrDefault(s => s.Id == category.Id);

                edit.Id = category.Id;
                edit.Title = category.Title;


                json = JsonConvert.SerializeObject(categories);

                if (String.IsNullOrEmpty(fileName2))
                    return;

                if (File.Exists(Path.Combine(folderPath2, fileName2)))
                {
                    File.WriteAllText(Path.Combine(folderPath2, fileName2), json);
                }
                else
                {
                    File.Create(Path.Combine(folderPath2, fileName2));
                }
            }

        }
        public async void AddProduct(Product product, Category SelectedCategory) 
        {
            if (product.Title != null && product.Title != "")
            {
                products = GetProductList().Result;
                if (SelectedCategory != null && SelectedCategory.Id != 0)
                    product.Category = SelectedCategory;
                products.Add(product);

                json = JsonConvert.SerializeObject(products);
                if (String.IsNullOrEmpty(fileName))
                    return;

                if (File.Exists(Path.Combine(folderPath, fileName)))
                {
                    File.WriteAllText(Path.Combine(folderPath, fileName), json);
                }
                else
                {
                    File.Create(Path.Combine(folderPath, fileName));
                }
            }

        }
        public async void AddCatigorie(List<Category> categories)
        {
            json = JsonConvert.SerializeObject(categories);
            if (String.IsNullOrEmpty(fileName2))
                return;

            if (File.Exists(Path.Combine(folderPath2, fileName2)))
            {
                File.WriteAllText(Path.Combine(folderPath2, fileName2), json);
            }
            else
            {
                File.Create(Path.Combine(folderPath2, fileName2));
            }

        }
        public async void DeleteProduct(Product product) 
        {
            products = GetProductList().Result;
            
            for(int i = 0; i < products.Count; i++)
            {
                    if (products[i].Id == product.Id)
                    products.RemoveAt(i);   
            }

            json = JsonConvert.SerializeObject(products);
            if (String.IsNullOrEmpty(fileName))
                return;

            if (File.Exists(Path.Combine(folderPath, fileName)))
            {
                File.WriteAllText(Path.Combine(folderPath, fileName), json);
            }
            else
            {
                File.Create(Path.Combine(folderPath, fileName));
            }

        }

        public async void DeleteCategory(Category category)
        {
            categories = GetCategoryList().Result;
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Id == category.Id)
                    categories.RemoveAt(i);
            }

            json = JsonConvert.SerializeObject(categories);

            if (String.IsNullOrEmpty(fileName2))
                return;

            if (File.Exists(Path.Combine(folderPath2, fileName2)))
            {
                File.WriteAllText(Path.Combine(folderPath2, fileName2), json);
            }
            else
            {
                File.Create(Path.Combine(folderPath2, fileName2));
            }

        }

        public void f()
        {
            json = JsonConvert.SerializeObject(products);
            if (String.IsNullOrEmpty(fileName))
                return;

            if (File.Exists(Path.Combine(folderPath, fileName)))
            {
                File.WriteAllText(Path.Combine(folderPath, fileName), json);
            }
            else
            {
                File.Create(Path.Combine(folderPath, fileName));
            }
        }
        public void c()
        {
            json = JsonConvert.SerializeObject(categories);
            if (String.IsNullOrEmpty(fileName2))
                return;

            if (File.Exists(Path.Combine(folderPath2, fileName2)))
            {
                File.WriteAllText(Path.Combine(folderPath2, fileName2), json);
            }
            else
            {
                File.Create(Path.Combine(folderPath2, fileName2));
            }
        }

    }

}
