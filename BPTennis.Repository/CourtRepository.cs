﻿using BPTennis.Data;
using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTennis.Repository
{
    public class CourtRepository
    {
        public List<Court> GetAllCourts()
        {
            using (var model = new bp_tennisEntities())
            {
                var courts = (from c in model.courts
                              select new Court()
                              {
                                  Id = c.Id,
                                  CourtName = c.name
                              }).ToList<Court>();
                return courts;
            }
        }

        public void CreateCourt(Court newCourt)
        {
            using (var model = new bp_tennisEntities())
            {
                BPTennis.Data.court dbCourt = new court()
                {
                    name = newCourt.CourtName
                };

                model.courts.Add(dbCourt);
                model.SaveChanges();
            }
        }

        public Court GetCourtById(int id)
        {
            using (var model = new bp_tennisEntities())
            {
                return (from c in model.courts
                       where c.Id == id
                       select new Court()
                            {    
                                Id = c.Id,
                                CourtName = c.name
                            }).FirstOrDefault();
            }
        }

        public void UpdateCourtDetails(Court court)
        {
            using (var model = new bp_tennisEntities())
            {
                var dbCourt = (from c in model.courts
                              where c.Id == court.Id
                              select c).FirstOrDefault();

                dbCourt.name = court.CourtName;

                model.SaveChanges();
            }    
        }

        public void RemoveCourt(int id)
        {
            using (var model = new bp_tennisEntities())
            {
                var dbCourt = (from c in model.courts
                               where c.Id == id
                               select c).FirstOrDefault();

                model.courts.Remove(dbCourt);
                model.SaveChanges();
            }
        }

    }
}
