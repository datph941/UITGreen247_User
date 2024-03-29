﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class Orders_user
    {
        public int id_ord { set; get; }
        public int id_user { set; get; }
        public int id_promotion { set; get; }
        public double ship { set; get; }
        public int paymethod { set; get; }
        public int status { set; get; }
        public string note { set; get; }
        public DateTime date { set; get; }
        public double price_sum { set; get; }
        public static int Insert(int id, int id_promotion, double ship, string note, int paymethod, double pricesum)
        {
            using (var context = new DataContext())
            {
                context.Orders_user.Add(new Orders_user
                {
                    id_user = id,
                    id_promotion = id_promotion,
                    ship = ship,
                    paymethod = paymethod,
                    status = 0,
                    note = note,
                    date = DateTime.Now,
                    price_sum = pricesum + ship
                });
                return context.SaveChanges();
            }
        }
        public static void DeleteOrder(int id)
        {
            using (var context = new DataContext())
            {
                var ord = (from p in context.Orders_user
                            where (p.id_ord == id)
                            select p).FirstOrDefault();
                if (ord != null)
                {
                    context.Remove(ord);
                    context.SaveChanges();
                }
            }
        }
        public static Orders_user SelectNew()
        {
            using (var context = new DataContext())
            {
                Orders_user ord = context.Orders_user.OrderBy(p=>p.id_ord).Last();
                return ord;
            }
        }
        public static List<Orders_user> Select(int id)
        {
            using (var context = new DataContext())
            {
                var list = context.Orders_user.Where(p=>p.id_user==id).ToList();
                return list;
            }
        }
        public static Orders_user SelectOne(int id)
        {
            using (var context = new DataContext())
            {
                var list = context.Orders_user.Where(p => p.id_ord == id).FirstOrDefault();
                return list;
            }
        }
        public static void UpdateStatus(int id, int status)
        {
            using (var context = new DataContext())
            {
                Orders_user orders = (from p in context.Orders_user
                                      where (p.id_ord == id)
                                      select p).FirstOrDefault();
                if (orders != null)
                {
                    orders.status = status;
                }
                context.SaveChanges();

            }
        }
    }
}
