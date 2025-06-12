using IVY.Application.DTOs.Filters;
using IVY.Application.Interfaces.IRepository;
using IVY.Domain.Enums;
using IVY.Domain.Models.Products;
namespace IVY.Application.Services;
public class Seed 
{
    private readonly IUnitOfWork uow;
    public Seed(IUnitOfWork uow)
    {
        this.uow = uow;
    } 
    private  DateTime RandomDateThisMonth()
    {
        var now = DateTime.Now;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);

        var range = (now - startOfMonth).TotalSeconds;
        var randomSeconds = Random.Shared.NextDouble() * range;

        return startOfMonth.AddSeconds(randomSeconds);
    }
    public void Data(){
        if(uow.Category.GetFirstOrDefault(x=>x.Category__Id>0)==null){
            InitCategory();
        }
        if(uow.Collection.GetFirstOrDefault(x=>x.Collection__Id>0)==null){
            InitCollection();
        }
        if(uow.Color.GetFirstOrDefault(x=>x.Color__Id>0)==null){
            InitColor();
        }
    }
    private void InitColor(){
        var colors= new List<Color>{
            new Color{
                Color__Name="Đen",
                Color__Image="black.png"
            },
            new Color{
                Color__Name="Trắng",
                Color__Image="white.png"
            },
            new Color{
                Color__Name="Xanh dương",
                Color__Image="blue.png"
            },
            new Color{
                Color__Name="Vàng",
                Color__Image="yellow.png"
            },
            new Color{
                Color__Name="Hồng",
                Color__Image="pink.png"
            },
            new Color{
                Color__Name="Đỏ",
                Color__Image="red.png"
            },
            new Color{
                Color__Name="Xám",
                Color__Image="gray.png"
            },
            new Color{
                Color__Name="Be",
                Color__Image="beige.png"
            },
            new Color{
                Color__Name="Nâu",
                Color__Image="brown.png"
            },
            new Color{
                Color__Name="Xanh lá",
                Color__Image="green.png"
            },
            new Color{
                Color__Name="Cam",
                Color__Image="orange.png"
            },
            new Color{
                Color__Name="Tím",
                Color__Image="purple.png"
            },
        };
        uow.Color.AddRange(colors);
    }
    private void InitCategory(){
        var category=new List<Category>{
            new Category{
            Category__Name="Áo",
            Category__Status=(int)ProductStatus.Releasing,
            Category__Type=(int)TypeOfCategory.Common,
            SubCategories= new List<SubCategory>
            {
               new SubCategory{
                    SubCategory__Name="Áo sơ mi",
                    SubCategory__Status=(int)ProductStatus.Releasing
               },
               new SubCategory{
                    SubCategory__Name="Áo thun",
                    SubCategory__Status=(int)ProductStatus.Releasing
               },
               new SubCategory{
                    SubCategory__Name="Áo croptop",
                    SubCategory__Status=(int)ProductStatus.Releasing
               },
               new SubCategory{
                    SubCategory__Name="Đồ lót",
                    SubCategory__Status=(int)ProductStatus.Releasing
               }
            }
            },
            new Category{
                Category__Name="Áo khoác",
                Category__Status=(int)ProductStatus.Releasing,
                Category__Type=(int)TypeOfCategory.Common,
                SubCategories= new List<SubCategory>{
                    new SubCategory{
                        SubCategory__Name="Áo vest/ blazer",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Áo dạ/ măng tô",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    }
                }
            },
            new Category{
                Category__Name="Set bộ",
                Category__Status=(int)ProductStatus.Releasing,
                Category__Type=(int)TypeOfCategory.Common,
                SubCategories= new List<SubCategory>{
                    new SubCategory{
                        SubCategory__Name="Set bộ công sở",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Set bộ co-ords",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Set bộ thun/ len",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    }
                }
            },
            new Category{
                Category__Name="Quần & Jumpsuit",
                Category__Status=(int)ProductStatus.Releasing,
                Category__Type=(int)TypeOfCategory.Common,
                SubCategories= new List<SubCategory>{
                    new SubCategory{
                        SubCategory__Name="Quần dài",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Quần jeans",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Quần lửng/ short",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Jumpsuit",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    }
                }
            },
            new Category{
                Category__Name="Chân váy",
                Category__Status=(int)ProductStatus.Releasing,
                Category__Type=(int)TypeOfCategory.Common,
                SubCategories= new List<SubCategory>{
                    new SubCategory{
                        SubCategory__Name="Chân váy bút chì",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Chân váy chữ A",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    }
                }
            },
            new Category{
                Category__Name="Đầm",
                Category__Status=(int)ProductStatus.Releasing,
                Category__Type=(int)TypeOfCategory.Common,
                SubCategories= new List<SubCategory>{
                    new SubCategory{
                        SubCategory__Name="Đầm công sở",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Đầm dự tiệc",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Đầm maxi",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Đầm suông",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    },
                    new SubCategory{
                        SubCategory__Name="Đầm xòe",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    }
                }
            },
            new Category{
                Category__Name="Senora",
                Category__Status=(int)ProductStatus.Releasing,
                Category__Type=(int)TypeOfCategory.Common,
                SubCategories= new List<SubCategory>{
                    new SubCategory {
                        SubCategory__Name="Senora",
                        SubCategory__Status=(int)ProductStatus.Releasing
                    }
                }}
        };
        uow.Category.AddRange(category);
            
    }
    private void InitCollection(){

       uow.Collection.AddRange(new List<Collection>{
        new Collection{
            Collection__Name="SUMMER TINT",
            Collection__Status=(int)ProductStatus.Releasing
        },
        new Collection{
            Collection__Name="LEAFLINE",
            Collection__Status=(int)ProductStatus.Releasing
        },
        new Collection{
            Collection__Name="DẠ VŨ",
            Collection__Status=(int)ProductStatus.Releasing
        },
        new Collection{
            Collection__Name="ROSIE CRUSH",
            Collection__Status=(int)ProductStatus.Releasing
        },
        new Collection{
            Collection__Name="BLUE SONATA",
            Collection__Status=(int)ProductStatus.Releasing
        },
        new Collection{
            Collection__Name="LILAS DREAM",
            Collection__Status=(int)ProductStatus.Releasing
        },
        new Collection{
            Collection__Name="SPRING GLOW",
            Collection__Status=(int)ProductStatus.Releasing
        },
        new Collection{
            Collection__Name="STARLIT JEWEL",
            Collection__Status=(int)ProductStatus.Releasing,
        },
       });
    }
}