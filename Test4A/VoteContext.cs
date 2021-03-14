using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test4A.Models;

namespace Test4A
{
    public interface IVoteDb
    {
        public FilmScoreModel Vote(int episode_id);
        public FilmScoreModel Vote(int episode_id, int vote);

    }
    public class VoteDbContext : DbContext, IVoteDb
    {
        public VoteDbContext()
        {
        }
        public VoteDbContext(DbContextOptions options): base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<FilmScoreModel> Films { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //   => options.UseSqlite(@"Data Source=c:\Temp\films.sqlite");

        public FilmScoreModel Vote(int episode_id)
        {
            return this.Find<FilmScoreModel>(episode_id) ?? new FilmScoreModel();
        }
        static object lockdb = new object();
        public FilmScoreModel Vote(int episode_id, int vote)
        {
            lock (lockdb)
            {
                var f = Find<FilmScoreModel>(episode_id);
                if (f == null)
                {
                    f = new FilmScoreModel
                    {
                        episode_id = episode_id,
                        count = 1,
                        sum = vote
                    };
                    Add(f);
                }
                else
                {
                    f.count++;
                    f.sum += vote;
                    Update(f);
                }
                SaveChanges();
                return f;
            }
        }
    }
}
