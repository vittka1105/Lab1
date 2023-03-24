using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal1 = new Journal
            {
                Name = "Journal of Computer Science",
                Frequency = "Monthly",
                ReleaseDate = new DateTime(2023, 3, 1),
                Circulation = 1000,
                Articles = new List<Article>()
            };

            Journal journal2 = new Journal
            {
                Name = "Journal of Physics",
                Frequency = "Bi-Monthly",
                ReleaseDate = new DateTime(2023, 3, 15),
                Circulation = 1500,
                Articles = new List<Article>()
            };

            Author author1 = new Author
            {
                Name = "John Smith",
                Organization = "XYZ Corporation",
                Articles = new List<Article>()
            };

            Author author2 = new Author
            {
                Name = "Mary Johnson",
                Organization = "ABC Institute",
                Articles = new List<Article>()
            };

            Article article1 = new Article
            {
                Title = "A Study of Algorithms",
                Authors = new List<Author> { author1 },
                Journal = journal1,
                SubmissionDate = new DateTime(2023, 2, 1)
            };

            Article article2 = new Article
            {
                Title = "The Theory of Relativity",
                Authors = new List<Author> { author2 },
                Journal = journal2,
                SubmissionDate = new DateTime(2023, 2, 15)
            };

            Article article3 = new Article
            {
                Title = "The Future of Artificial Intelligence",
                Authors = new List<Author> { author1, author2 },
                Journal = journal1,
                SubmissionDate = new DateTime(2023, 3, 1)
            };

              journal1.Articles.Add(article1);
              journal1.Articles.Add(article3);
              journal2.Articles.Add(article2);
              author1.Articles.Add(article1);
              author1.Articles.Add(article3);
              author2.Articles.Add(article2);
              author2.Articles.Add(article3);
            Publication publication = new Publication()
            {
                Journals = new List<Journal>() { journal1, journal2 },
                Authors = new List<Author>() { author1, author2 }
            };
            //1)Запит на виведення назв журналів з частотою виходу "Bi-Monthly". 
            var monthlyJournals = from journal in publication.Journals
                                  where journal.Frequency == "Bi-Monthly"
                                  select journal;
            foreach (var journal in monthlyJournals)
            {
                Console.WriteLine(journal.Name);
            }
            Console.WriteLine("========================================================================================");

            //2)Групування журналів за частотою випуску та підрахунок кількості журналів у кожній групі.
            var journalsByFrequency = publication.Journals.GroupBy(journal => journal.Frequency)
                                               .Select(frequencyGroup => new { Frequency = frequencyGroup.Key, Count = frequencyGroup.Count() });
            foreach (var group in journalsByFrequency)
            {
                Console.WriteLine($"Frequency: {group.Frequency}, Count: {group.Count}");
            }

            Console.WriteLine("========================================================================================");
            //3)Групування статей за журналом та підрахунок кількості статей в кожній групі.
            var articlesByJournal = from article in publication.Authors.SelectMany(author => author.Articles)
                                    group article by article.Journal into journalGroup
                                    select new { Journal = journalGroup.Key, Count = journalGroup.Count() };
            foreach (var item in articlesByJournal)
            {
                Console.WriteLine($"{item.Journal.Name}: {item.Count} articles");
            }

            Console.WriteLine("========================================================================================");
            //4)Запит на сортування авторів за кількістю статей, які вони написали, в порядку спадання:
            var sortedAuthors = from author in publication.Authors
                                orderby author.Articles.Count() descending
                                select author;
            foreach (var author in sortedAuthors)
            {
                Console.WriteLine($"Author: {author.Name}, Articles: {author.Articles.Count}");
            }


            Console.WriteLine("========================================================================================");
            //5)Відсортувати журнали за датою випуску у зростаючому порядку та вивести їх назви та дати випуску.
            var journalsByReleaseDate = from journal in publication.Journals
                                        orderby journal.ReleaseDate
                                        select new { Name = journal.Name, ReleaseDate = journal.ReleaseDate };

            foreach (var journalInfo in journalsByReleaseDate)
            {
                Console.WriteLine($"{journalInfo.Name}: {journalInfo.ReleaseDate}");
            }

            Console.WriteLine("========================================================================================");
            //6)Згрупувати статті за датою подання та вивести заголовки статей у кожній групі:
            var articlesByDate = from article in publication.Journals.SelectMany(journal => journal.Articles)
                                 group article by article.SubmissionDate into dateGroup
                                 select new { Date = dateGroup.Key, Articles = dateGroup.OrderBy(article => article.Title) };
            foreach (var group in articlesByDate)
            {
                Console.WriteLine($"Date: {group.Date}");
                foreach (var article in group.Articles)
                {
                    Console.WriteLine(article.Title);
                }
            }
            Console.WriteLine("========================================================================================");
            //7)Згрупувати журнали за роком випуску та вивести назву та дату першого випуску кожної групи:
            var journalsByYear = from journal in publication.Journals
                                 group journal by journal.ReleaseDate.Year into yearGroup
                                 select new { Year = yearGroup.Key, Journals = yearGroup.OrderBy(journal => journal.ReleaseDate) };
            foreach (var group in journalsByYear)
            {
                Console.WriteLine($"Year: {group.Year}");
                foreach (var journal in group.Journals)
                {
                    Console.WriteLine($"{journal.Name} - {journal.ReleaseDate}");
                }
            }

            Console.WriteLine("========================================================================================");
            //8)Згрупувати авторів за назвою організації та вивести кількість статей у кожній групі:
            var articlesByOrganization = from author in publication.Authors
                                         from article in author.Articles
                                         group article by author.Organization into organizationGroup
                                         select new { Organization = organizationGroup.Key, Count = organizationGroup.Count() };
            foreach (var group in articlesByOrganization)
            {
                Console.WriteLine($"{group.Organization}: {group.Count}");
            }

            Console.WriteLine("========================================================================================");
            //9)Знайти мінімальний рік видання серед всіх журналів:
            var minYear = publication.Journals
                         .Aggregate(DateTime.MaxValue.Year, (acc, journal) => Math.Min(acc, journal.ReleaseDate.Year));
            Console.WriteLine($"The earliest year of publication in the list of journals is: {minYear}");

            Console.WriteLine("========================================================================================");

            //10)Знайти максимальну кількість статей серед всіх авторів:
            var maxArticles = publication.Authors
                         .Aggregate(0, (acc, author) => Math.Max(acc, author.Articles.Count));
            Console.WriteLine($"The maximum number of articles by an author is {maxArticles}");

            Console.WriteLine("========================================================================================");

            //11)Знайти суму тиражу всіх журналів:
            var totalCirculation = publication.Journals
                         .Aggregate(0, (acc, journal) => acc + journal.Circulation);
            Console.WriteLine($"Total circulation of all journals: {totalCirculation}");
            
            Console.WriteLine("========================================================================================");

            //12)Класичний варіант з групуванням за журналом та знаходженням середнього значення обсягу журналу:
            var query1 = from journal in publication.Journals
                         group journal by journal.Name into journalGroup
                         select new
                         {
                             JournalName = journalGroup.Key,
                             AvgCirculation = journalGroup.Average(j => j.Circulation)
                         };
            foreach (var journal in query1)
            {
                Console.WriteLine($"Journal name: {journal.JournalName}, Average circulation: {journal.AvgCirculation}");
            }

            Console.WriteLine("========================================================================================");

            //13)Запит для виведення всіх статей разом з авторами,що їх написали
            var articlesWithAuthors = publication.Journals.SelectMany(journal => journal.Articles)
                                                  .Join(publication.Authors, article => article.Authors
                                                 .FirstOrDefault(), author => author, (article, author) => new { Article = article, Author = author });
            foreach (var item in articlesWithAuthors)
            {
                Console.WriteLine($"Article: {item.Article.Title}");
                Console.WriteLine($"Author: {item.Author.Name}");
            }
            Console.WriteLine("========================================================================================");

            // 14)Знайти чи є статті, подані в журналах з частотою видання "Monthly".
            var anyMonthlyArticles = publication.Journals
                        .Where(journal => journal.Frequency == "Monthly")
                        .Select(journal => journal.Articles.Any());
            foreach (var item in anyMonthlyArticles)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("========================================================================================");

            //15)Знайти журнали, у яких було більше двох статей.
            var journalsWithMoreThanOneArticles = from journal in publication.Journals
                                                  where journal.Articles.Count > 1
                                                  select journal;
            foreach (var journal in journalsWithMoreThanOneArticles)
            {
                Console.WriteLine(journal.Name);
            }

            Console.WriteLine("========================================================================================");

            //16)Знайти статті, які були надруковані в обох журналах:
            var articlesInBothJournals = publication.Journals.SelectMany(j => j.Articles)
                                             .GroupBy(article => article)
                                             .Where(articlesByJournal => articlesByJournal.Select(a => a.Journal.Name).Distinct().Count() == 2)
                                             .Select(articlesByJournal => articlesByJournal.Key).Distinct();
            foreach (var article in articlesInBothJournals)
            {
                Console.WriteLine(article.Title);
            }
            if (!articlesInBothJournals.Any())
            {
                Console.WriteLine("There are no articles published in both journals.");
            }

            Console.WriteLine("========================================================================================");

            //17)Сортування журналів за частотою видання, потім за тиражем у зростаючому порядку
            var sortedJournals = publication.Journals.OrderBy(journal => journal.Frequency)
                        .ThenBy(journal => journal.Circulation);
            foreach (var journal in sortedJournals)
            {
                Console.WriteLine("Journal: {0}, Frequency: {1}, Circulation: {2}",
                    journal.Name, journal.Frequency, journal.Circulation);
            }

            Console.WriteLine("========================================================================================");

            //18)Кількість статей, що були опубліковані у кожному журналі згрупованих за роком та місяцем видання журналу:
            var articlesCountByYearMonth = publication.Journals
                                             .SelectMany(journal => journal.Articles, (journal, article) => new { Journal = journal, Article = article })
                                             .GroupBy(ja => new { ja.Journal.ReleaseDate.Year, ja.Journal.ReleaseDate.Month })
                                             .Select(articleGroup => new { Year = articleGroup.Key.Year, Month = articleGroup.Key.Month, Count = articleGroup.Count() });


            foreach (var group in articlesCountByYearMonth)
            {
                Console.WriteLine($"Year: {group.Year}, Month: {group.Month}, Count: {group.Count}");
            }

            Console.WriteLine("========================================================================================");

            //19)Вивести всі авторів, крім авторів, які публікувалися в журналі "Journal of Physics".
            var authorsExceptJournal2 = publication.Authors.Except(
                                          publication.Journals.Single(journal => journal.Name == "Journal of Physics")
                                          .Articles.SelectMany(article => article.Authors)).Distinct();
            foreach (var author in authorsExceptJournal2)
            {
                Console.WriteLine($"{author.Name}, {author.Organization}");
            }

            Console.WriteLine("========================================================================================");

            //20)Список найбільш популярних авторів за кількістю публікацій, згрупованих за назвою їхньої організації:
            var popularAuthorsByOrg = publication.Authors
                                          .GroupBy(a => a.Organization)
                                           .Select(orgGroup => new
                                                                  {
                                                                      Organization = orgGroup.Key,
                                                                      TotalPublications = orgGroup.SelectMany(a => a.Articles).Count(),
                                                                      PopularAuthors = orgGroup.OrderByDescending(a => a.Articles.Count).Select(a => a.Name)
                                                                  })
                                          .OrderByDescending(org => org.TotalPublications);

            foreach (var org in popularAuthorsByOrg)
            {
                Console.WriteLine($"Organization: {org.Organization}");
                Console.WriteLine($"Total Publications: {org.TotalPublications}");

                Console.WriteLine("Popular Authors:");
                foreach (var author in org.PopularAuthors)
                {
                    Console.WriteLine($"- {author}");
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
