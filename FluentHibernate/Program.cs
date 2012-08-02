using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entity;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace FluentNhibernateTest
{
    public class FluentNhibernate
    {
        private static ISessionFactory sessionFactory { get; set; }

        public static ISessionFactory GetCurrentFactory()
        {
            if (sessionFactory == null)
            {
                sessionFactory = CreateSessionFactory();
            }

            return sessionFactory;
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008.ShowSql()
                        .ConnectionString(s => s.Server(@".\SQLEXPRESS")
                                                   .Database("MyNorthwind")
                                                   .TrustedConnection())
                )
                //单个类注册
                //.Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(EntityMap.ProductMap))).ExportTo(@"c:\"))
                //命名空间注册
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load("EntityMap")).ExportTo(@"c:\"))
                .BuildSessionFactory(); 
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();

            ISessionFactory sessionFactory = FluentNhibernate.GetCurrentFactory();

            using (ISession session = sessionFactory.OpenSession())
            {
                //Create(session);

                //Select(session);

                Update(session);
                

                //Delete(session);

            }

            #region comment  code

            /*using (IStatelessSession session = sessionFactory.OpenStatelessSession())
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    try
                    {
                        Product product = new Product();
                        product.CreateTime = DateTime.Now;
                        product.Name = "First Product";
                        product.Price = 15;
                        //新增
                        session.Save(product);

                        //删除
                        session.Delete(product);
                        //session.Flush();

                        product.Name = "Barney";
                        session.Update(product);
                        session.Flush();

                        Product p = session.Load<Product>(10);
                        Console.WriteLine(p.Name);

                        Order o = new Order();
                        o.OrderID = "1234";
                        o.ProductID = 1;
                        session.Insert(o);

                        //IList<Product> l = session.CreateQuery(" from Product").List<Product>();
                        //foreach (Product p in l)
                        //{
                        //    Console.WriteLine(p.Name);
                        //    //Console.WriteLine(p.categorie.Name);
                        //}

                        //IList<Categorie> c = session.CreateQuery(" from Categorie").List<Categorie>();

                        //foreach (Categorie category in c)
                        //{
                        //    Console.WriteLine(category.Name);
                        //}


                        //必须Flush()之后才能提交
                        tran.Commit();
                    }
                    catch (HibernateException)
                    {
                        tran.Rollback();
                    }*/

            #endregion
        }

        private static void Update(ISession session)
        {
            ModifyOrderDetail(session);
        }
         
        private static void ModifyOrderDetail(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    Order order = session.Get<Order>(60);

                    IList<OrderDetails> orderDetailses = order.details;
                    foreach (OrderDetails orderDetailse in orderDetailses)
                    {
                        orderDetailse.UnitPrice = 222;
                    }

                    //session.SaveOrUpdate(order);
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                } 
            }
        }

        private static void Delete(ISession session)
        {
            //DeleteProduct(session);
            DeleteCategories(session);
            //DeleteCustomer(session);
            //DeleteOrder(session);
            //DeleteGoldOrder(session);
        }

        //同样可以删除掉GoldOrder里面的数据
        private static void DeleteOrder(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    var order = session.Get<Order>(57);

                    if (order != null)
                    {
                        session.Delete(order);
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        //同样可以删除掉Order里面的数据
        private static void DeleteGoldOrder(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    var order = session.Get<GoldOrder>(58);

                    if (order != null)
                    {
                        session.Delete(order);
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private static void DeleteCustomer(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    var cus = session.Get<Customer>(95);

                    if (cus != null)
                    {
                        session.Delete(cus);
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private static void DeleteCategories(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                Categorie jimtest = session.Get<Categorie>(24);

                if (jimtest != null)
                {
                    session.Delete(jimtest);
                    tran.Commit();
                }
            }
        }

        /*
         * 删除包括有订单的产品
         * todo: 备注：
         * 1，不能删除与之的订单信息。只会删除其关联项,因为是多对多的关系。
         * 2，在删除产品是，会把类别也给删除掉。已经解决，将Products 的Cascade.All()改成Cascade.SaveUpdate()就OK了。
         */
        private static void DeleteProduct(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    Product pear = session.Get<Product>(650);

                    if (pear != null)
                    {
                        session.Delete(pear);
                        tran.Commit();
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private static void Create(ISession session)
        {
            //CreateCategorieAndProduct(session);
            //CreateCustomer(session);
            //CreateOrder(session);
            //CreateGoldOrder(session); 
        }

        private static void CreateCustomer(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    Customer cus = new Customer();
                    cus.Name = "jiguixin";
                    cus.ContactName = "123456789";
                    cus.ContactInfo = new CustomerContactInfo()
                                          {Address = "zhongbajie", PostalCode = "610000", Tel = "81402373"};
                    cus.Detail = new CustomerDetail()
                                     {
                                         CreateTime = DateTime.Now,
                                         CustomerEmail = "jim@test.com",
                                         LastUpdated = DateTime.Now.AddDays(22),
                                         customer = cus
                                     };

                    session.SaveOrUpdate(cus);

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private static void CreateOrder(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    Customer customer = session.Get<Customer>(95);
                    Product product = session.Get<Product>(652);
                    Product product1 = session.Get<Product>(653);

                    Order order = new Order()
                                      {
                                          OrderDate = DateTime.Now,
                                          Comment = "jim Test Create Order111",
                                          customer = customer,
                                          Finished = false,
                                          State = OrderState.Create,
                                          SumMoney = (decimal) 1556.55,                
                                      };
                     
                    var od1 = new OrderDetails()
                    {
                        product = product,
                        order = order,
                        Quantity = 10,
                        UnitPrice = (Decimal)22.2
                    };

                    var od2 = new OrderDetails()
                    {
                        product = product1,
                        order = order,
                        Quantity = 20,
                        UnitPrice = (Decimal)32.2
                    };

                    order.details.Add(od1);
                    order.details.Add(od2);

                   

                    session.Save(order);

                    #region 会报警告

                    //Todo:Unable to determine if Entity.OrderDetails with assigned identifier Entity.OrderDetails is transient or detached; querying the database. Use explicit Save() or Update() in session to prevent this.

                    //List<OrderDetails> details = new List<OrderDetails>()
                    //                             {
                    //                                 new OrderDetails()
                    //                                     {
                    //                                         product = product,
                    //                                         order = order,
                    //                                         Quantity = 10,
                    //                                         UnitPrice = (Decimal) 22.2
                    //                                     },
                    //                                       new OrderDetails()
                    //                                     {
                    //                                         product = product1,
                    //                                         order = order,
                    //                                         Quantity = 20,
                    //                                         UnitPrice = (Decimal) 32.2
                    //                                     },
                    //                             };
                    //order.details = details;
                    //session.SaveOrUpdate(order);

                    #endregion

                  /*  var od1 = new OrderDetails()
                                  {
                                      product = product,
                                      order = order,
                                      Quantity = 10,
                                      UnitPrice = (Decimal) 22.2
                                  };

                    session.Save(od1);
                    var od2 = new OrderDetails()
                                  {
                                      product = product1,
                                      order = order,
                                      Quantity = 20,
                                      UnitPrice = (Decimal) 32.2
                                  };
                    session.Save(od2);*/

                    #region 不加这2句就不会多执行2次更新操作
                     
                    /*
                     UPDATE [Order Details]
                     SET    OrderID = 51 /* @p0 #1#
                     WHERE  OrderID = 51 /* @p1 #1#
                            AND ProductID = 650 /* @p2 #1#
                     */

                    //order.details = new List<OrderDetails>();
                    //order.details.Add(od1);
                    //order.details.Add(od2);

                    #endregion

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();

                    throw;
                }
            }
        }

        private static void CreateGoldOrder(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    Customer customer = session.Get<Customer>(96);
                    Product product = session.Get<Product>(654);
                    Product product1 = session.Get<Product>(655);

                    GoldOrder order = new GoldOrder()
                    {
                        OrderDate = DateTime.Now,
                        Comment = "jim Test Create Order111",
                        customer = customer,
                        Finished = false,
                        State = OrderState.Create,
                        SumMoney = (decimal)1556.55,
                    };

                    var od1 = new OrderDetails()
                    {
                        product = product,
                        order = order,
                        Quantity = 10,
                        UnitPrice = (Decimal)22.2
                    };

                    var od2 = new OrderDetails()
                    {
                        product = product1,
                        order = order,
                        Quantity = 20,
                        UnitPrice = (Decimal)32.2
                    };

                    order.details.Add(od1);
                    order.details.Add(od2);

                    order.CharacterName = "举手别对";
                    order.GoldCount = 1000;
                     
                    session.Save(order);
                       
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();

                    throw;
                }
            }
        }

        #region 插入类别与产品
         
        //加了Cascade.SaveUpdate(),直接保存产品，类别也自动保存成功了。
        private static void CreateCategorieAndProduct2(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    var cat = new Categorie {Name = "Jim Test2"};
                      
                    var pro1 = new Product
                                   {
                                       categorie = cat,
                                       Name = "苹果2",
                                       Price = 22,
                                       Quantity = 22,
                                       Remark = "苹果Test2",
                                       Unit = "个"
                                   };

                    var pro2 = new Product
                                   {
                                       categorie = cat,
                                       Name = "梨子2",
                                       Price = 33,
                                       Quantity = 33,
                                       Remark = "梨子Test2",
                                       Unit = "个"
                                   };

                    session.SaveOrUpdate(pro1);
                    session.SaveOrUpdate(pro2);
                     
                    session.Flush();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        //加了Cascade.SaveUpdate()，直接保存Categorie就把产品保存成功了
        private static void CreateCategorieAndProduct(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    var cat = new Categorie { Name = "Jim Test2" };
                     
                    var pro1 = new Product
                    {
                        categorie = cat,
                        Name = "苹果2",
                        Price = 22,
                        Quantity = 22,
                        Remark = "苹果Test2",
                        Unit = "个"
                    };

                    var pro2 = new Product
                    {
                        categorie = cat,
                        Name = "梨子2",
                        Price = 33,
                        Quantity = 33,
                        Remark = "梨子Test2",
                        Unit = "个"
                    };

                    cat.Products = new List<Product> { pro1, pro2 };

                    session.SaveOrUpdate(cat);

                    //session.SaveOrUpdate(pro1);
                    //session.SaveOrUpdate(pro2);

                    session.Flush();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        //不加Cascade 级联操作，就必须先保存Categorie再保存Product
        private static void CreateCategorieAndProduct1(ISession session)
        {
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    var cat = new Categorie { Name = "Jim Test" };

                    session.SaveOrUpdate(cat);

                    var pro1 = new Product
                    {
                        categorie = cat,
                        Name = "苹果",
                        Price = 22,
                        Quantity = 22,
                        Remark = "苹果Test",
                        Unit = "个"
                    };

                    var pro2 = new Product
                    {
                        categorie = cat,
                        Name = "梨子",
                        Price = 33,
                        Quantity = 33,
                        Remark = "梨子Test",
                        Unit = "个"
                    };

                    session.SaveOrUpdate(pro1);
                    session.SaveOrUpdate(pro2);

                    //cat.Products = new List<Product> { pro1, pro2 };
                     
                    session.Flush();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        #endregion

        private static void Select(ISession session)
        {
            using (var tran = session.BeginTransaction())
            {
                //延迟加载,如果没有这条数据会报错
                Customer cus = session.Load<Customer>(97);
                //只有在调用的时候才会去查
                Console.WriteLine(cus.Name); 
/*
                IList<Categorie> c = session.CreateQuery(" from Categorie").SetFirstResult(0).SetMaxResults(10).List<Categorie>().OrderByDescending(o => o.CategoryID).ToList();

                foreach (Categorie category in c)
                {
                    Console.WriteLine(category.Name); 
                }

                IList<Product> l = session.CreateQuery(" from Product").List<Product>();
                foreach (Product p in l)
                {
                    Console.WriteLine(p.Name);
                    //Console.WriteLine(p.categorie.Name);
                }

                IList<Order> orders = session.CreateQuery(" from Order").List<Order>();

                foreach (Order order in orders)
                {
                    Console.WriteLine(order.OrderID);
                }

                IList<GoldOrder> goldOrder = session.CreateQuery(" from GoldOrder").List<GoldOrder>();

                foreach (GoldOrder order in goldOrder)
                {
                    Console.WriteLine(order.CharacterName);
                }

                //会出现Select N+1 问题
                //IList<Customer> customerInfo = session.CreateQuery(" from Customer").List<Customer>();

                //使用SetFetchMode 就将联合的数据一起查询出来，不会在循环中去读取数据。
                IList<Customer> customerInfo = session.CreateCriteria(typeof(Customer)).SetFetchMode("Detail",FetchMode.Eager).List<Customer>();

                foreach (Customer customer in customerInfo)
                {
                    Console.WriteLine(customer.Name);
                }
 */

                tran.Commit();
            }
        }
    }
}