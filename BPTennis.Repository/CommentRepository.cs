using BPTennis.Data;
using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTennis.Repository
{
    public class CommentRepository
    {
        public void AddComment(string name, string comment)
        {
            using (var model = new bp_tennisEntities())
            {
                BPTennis.Data.comments dbComment = new comments()
                {
                    name = name,
                    comment = comment
                };

                model.comments1.Add(dbComment);
                model.SaveChanges();
            }
        }
        public List<Comments> GetAllComments()
        {
            using (var model = new bp_tennisEntities())
            {
                var comment = (from c in model.comments1
                               select new Comments
                               {
                                   Name = c.name,
                                   Comment = c.comment
                               }).ToList<Comments>();
                return comment;

            }

        }

    }
}
