using Azure;
using Ghasedak.Core;
using Microsoft.EntityFrameworkCore;
using ReadAndAnalysis.App.DTOs.News;
using ReadAndAnalysis.App.Extensions;
using ReadAndAnalysis.App.Generators;
using ReadAndAnalysis.App.Services.Interfaces;
using ReadAndAnalysis.Data.Entities;

namespace ReadAndAnalysis.App.Services.Implementations
{
    public class NewsService : INewsService
    {
        private TxtPrcContext _context;
        public NewsService(TxtPrcContext context)
        {
            _context = context;
        }

        public async Task AddEvalutedResult(long newsId, long fieldchild, long? source, long estimate,
            long userId, long? relevance)
        {
            EvaluatedResult res = new EvaluatedResult()
            {
                EstimateId = estimate,
                FieldOfUseId = fieldchild,
                NewsId = newsId,
                UserId = userId,
                StatisticalSourceId = (long)source,
                RelevanceId = relevance,
                CreateDate = DateTime.Now
            };
            await _context.EvaluatedResults.AddAsync(res);
            await _context.SaveChangesAsync();
        }

        public async Task AddInsertedsNews(CreateInsertedNewsDto create, long userId)
        {
            NewsInserted insert = new NewsInserted()
            {
                Gid = Guid.NewGuid(),
                ManagementSourceId = create.ManagementSourceId,
                NewsUrl = create.NewsUrl,
                NewsTitle = create.NewsTitle,
                NewsPublishedDate = create.NewsPublishedDate,
                NewsDescription = create.NewsDescription,
                EstimateId = create.EstimateId,
                CreateDate = DateTime.Now,
                IsDelete = false,
                EstimateDscp = create.EstimateDscp,
                UserCreatedId = userId,
                UserCreatedIp = GetIpAddress.GetIp(),
                NegativeReasonId = create.NegativeReasonId
            };
            await _context.NewsInserteds.AddAsync(insert);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddMainKeyWord(string title, long userId)
        {
            KeyWordsMain key = new KeyWordsMain()
            {
                CreateDate = DateTime.Now,
                Title = title,
                CreateBy = userId,
                CreateIp = GetIpAddress.GetIp(),
                Gid = Guid.NewGuid()
            };
            await _context.KeyWordsMains.AddAsync(key);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CanAddEvalutedResult(long newsId)
        {
            return await _context.EvaluatedResults.AnyAsync(s => s.NewsId == newsId);
        }

        public async Task DeleteEvaluated(long newsId)
        {
            var evaluted = await GetEvaluatedResult(newsId);
            if (evaluted != null)
            {
                _context.EvaluatedResults.Remove(evaluted);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOilNews(long newsId, long userId)
        {
            var news = await _context.NewsRssReeds.SingleAsync(n => n.Id == newsId);
            news.NotOil = 1;
            news.UserIdThatMakeIdNotOil = userId;
            _context.NewsRssReeds.Update(news);

            await _context.SaveChangesAsync();
        }

        public async Task EditInsertedNews(InsertedNewsListDto insert, long userId)
        {
            var inserted = await _context.NewsInserteds.SingleAsync(n => n.Id == insert.Id);
            inserted.NewsTitle = insert.NewsTitle;
            inserted.NewsDescription = insert.NewsDescription;
            inserted.NewsPublishedDate = insert.NewsPublishedDate;
            inserted.NewsUrl = insert.NewsUrl;
            inserted.EstimateId = insert.EstimateId;
            inserted.ManagementSourceId = insert.ManagementSourceId;
            inserted.ModifiedUserId = userId;
            inserted.ModifiedDate = DateTime.Now;
            inserted.ModifiedIp = GetIpAddress.GetIp();
            inserted.EstimateDscp = insert.EstimateDscp;
            inserted.NegativeReasonId = insert.NegativeReasonId;
            _context.NewsInserteds.Update(inserted);
            await _context.SaveChangesAsync();
        }

        public async Task<List<InsertedNewsListDto>> GetAllInsertedNews(string? startDate
            , string? endDate, int? estimateId)
        {
            var inserteds = await _context.NewsInserteds.Where(d => !d.IsDelete).ToListAsync();
            if (startDate != null)
            {
                DateTime start = startDate.ToMiladi();
                inserteds = inserteds.Where(d =>
                d.NewsPublishedDate.ToMiladi() >= start).ToList();
            }
            if (endDate != null)
            {
                DateTime end = endDate.ToMiladi();
                inserteds = inserteds.Where(d =>
                 d.NewsPublishedDate.ToMiladi() <= end).ToList();
            }
            if (estimateId != null)
            {
                inserteds = inserteds.Where(d => d.EstimateId == estimateId).ToList();
            }

            List<InsertedNewsListDto> list = new List<InsertedNewsListDto>();
            foreach (var item in inserteds)
            {
                var inserted = await _context.NewsInserteds.SingleAsync(i => i.Id == item.Id);
                InsertedNewsListDto dto = new InsertedNewsListDto()
                {
                    Id = item.Id,
                    EstimateId = item.EstimateId,
                    NewsDescription = item.NewsDescription,
                    NewsPublishedDate = item.NewsPublishedDate,
                    NewsTitle = item.NewsTitle,
                    NewsUrl = item.NewsUrl,
                    ManagementSourceId = item.ManagementSourceId
                };
                list.Add(dto);
            }
            return list;
        }

        public async Task<List<MainKeyWordListDto>> GetAllMainKeyWords()
        {
            List<MainKeyWordListDto> list = new List<MainKeyWordListDto>();
            var words = await _context.KeyWordsMains.ToListAsync();
            foreach (var word in words)
            {
                MainKeyWordListDto dto = new MainKeyWordListDto()
                {
                    Id = word.Id,
                    Title = word.Title
                };
                list.Add(dto);
            }
            return list;
        }

        public async Task<List<OilNewsListDto>> GetAllOilNews(string? starDate, string? endDate)
        {
            var start = starDate.ToMiladi();
            var end = endDate.ToMiladi();

            List<OilNewsListDto> list = new List<OilNewsListDto>();

            var evaluateds = await _context.EvaluatedResults.Select(c => c.NewsId).ToListAsync();

            var newsList = await _context.NewsRssReeds.Where(n => (bool)n.IsOil).ToListAsync();
            if (starDate != null && endDate != null)
            {
                newsList = newsList.Where(n => n.NotOil == 0 &&
                n.PublishedDate != null
                && n.PublishedDate.ToMiladiInverse() > start &&
            n.PublishedDate.ToMiladiInverse() <= end).ToList();
            }


            foreach (var news in newsList)
            {
                if (!evaluateds.Contains(news.Id))
                {
                    var site = news.LinkUrl.Split("/");
                    OilNewsListDto dto = new OilNewsListDto()
                    {
                        Id = news.Id,
                        Title = news.Title,
                        LinkUrl = news.LinkUrl,
                        Description = news.Description,
                        TitleUrl = site[2].ToString(),
                        CreateDate = news.CreateDate,
                        PublishedDate = news.PublishedDate,

                    };
                    list.Add(dto);
                }
            }


            return list;
        }



        public async Task<KeyWordsUsedInOilNewsDto> GetAllUsedKeyWords(long newsId)
        {
            var news = await _context.NewsRssReedProcs.Where(n => n.NewsRssReedId == newsId).ToListAsync();
            KeyWordsUsedInOilNewsDto dto = new KeyWordsUsedInOilNewsDto();
            dto.NewsId = newsId;
            List<string> mains = new List<string>();
            foreach (var item in news)
            {
                mains.Add(item.KeyWordsMainTitle);
            }
            dto.MainKeys = mains;
            List<string> second = new List<string>();
            foreach (var item in news)
            {
                second.Add(item.KeyWordsTitle);
            }
            dto.SecondStepKeys = second;
            return dto;
        }

        public async Task<List<FieldOfUse>> GetChildFieldOfUse(long parent)
        {
            return await _context.FieldOfUse.Where(f => f.ParentId == parent).ToListAsync();
        }

        public async Task<List<EstimateNews>> GetEstimateNews()
        {
            return await _context.EstimateNews.ToListAsync();
        }

        public async Task<EvaluatedResult?> GetEvaluatedResult(long newsId)
        {
            return await _context.EvaluatedResults.SingleOrDefaultAsync(e => e.NewsId == newsId);
        }

        public async Task<string?> GetEvalutedFieldTitle(long newsId)
        {
            var eva = await GetEvaluatedResult(newsId);
            string title = "";
            if (eva != null)
            {
                var field = await _context.FieldOfUse.SingleOrDefaultAsync(f => f.Id == eva.FieldOfUseId);
                var parent = await _context.FieldOfUse.FirstOrDefaultAsync(f => f.Id == field.ParentId);
                title = parent.Name + " - " + field.Name;
            }
            return title;
        }

        public async Task<List<EvalutedListDto>> GetEvalutedNews(string? startDate, string? endDate, long? estimateId)
        {
            List<EvalutedListDto> list = new List<EvalutedListDto>();
            var evaluteds = await _context.EvaluatedResults
                .Include(e => e.News).ToListAsync();
            if (startDate != null)
            {
                DateTime start = startDate.ToMiladi();
                evaluteds = evaluteds.Where(d =>
                Convert.ToDateTime(d.News.PublishedDate) >= start).ToList();
            }
            if (endDate != null)
            {
                DateTime end = endDate.ToMiladi();
                evaluteds = evaluteds.Where(d =>
               Convert.ToDateTime(d.News.PublishedDate) <= end).ToList();
            }
            if (estimateId != null)
            {
                evaluteds = evaluteds.Where(e => e.EstimateId == estimateId).ToList();
            }
            foreach (var item in evaluteds)
            {
                var news = await _context.NewsRssReeds.SingleAsync(r => r.Id == item.NewsId);
                var isSendForSms = await _context.NegativeOilNewsForSendingSms.SingleOrDefaultAsync(s => s.NewsId == news.Id);
                bool isSended = false;
                if (isSendForSms != null)
                    isSended = true;
                var site = news.LinkUrl.Split("/");
                EvalutedListDto dto = new EvalutedListDto()
                {

                    Id = news.Id,
                    Title = news.Title,
                    LinkUrl = news.LinkUrl,
                    Description = news.Description,
                    TitleUrl = site[2].ToString(),
                    PublishedDate = news.PublishedDate,
                    EstimateId = item.EstimateId,
                    //Estimate =  item.Estimate?.Name,
                    FieldOfUseId = item.FieldOfUseId,
                    FieldOfUse = _context.FieldOfUse.SingleOrDefaultAsync(s => s.Id == item.FieldOfUseId).Result?.Name,
                    StatisticalSourceId = item.StatisticalSourceId,
                    StatisticalSource = _context.StatisticalSources.SingleOrDefaultAsync(s => s.Id == item.StatisticalSourceId).Result?.Name,
                    IsSendForSms = isSended
                };
                list.Add(dto);

            }
            return list;
        }

        public async Task<List<FieldOfUse>> GetFieldOfUse()
        {
            return await _context.FieldOfUse.Where(f => f.ParentId == null).ToListAsync();
        }

        public async Task<InsertedNewsListDto> GetInsertedById(long id)
        {
            var inserted = await _context.NewsInserteds.SingleOrDefaultAsync(r => r.Id == id);
            InsertedNewsListDto dto = new InsertedNewsListDto();
            if (inserted != null)
            {
                dto.Id = inserted.Id;
                dto.NewsTitle = inserted.NewsTitle;
                dto.NewsDescription = inserted.NewsDescription;
                dto.NewsUrl = inserted.NewsUrl;
                dto.EstimateId = inserted.EstimateId;
                dto.ManagementSourceId = inserted.ManagementSourceId;
                dto.NewsPublishedDate = inserted.NewsPublishedDate;
                dto.EstimateDscp = inserted.EstimateDscp;
                dto.NegativeReasonId = inserted.NegativeReasonId;
            }
            return dto;
        }

        public async Task<string?> GetLastPublishedDate(long userId)
        {
            var lastNews = await _context.NewsInserteds.Where(n => n.UserCreatedId == userId)
                  .OrderByDescending(n => n.Id).ToListAsync();
            if (lastNews == null)
            {
                return string.Empty;
            }
            return lastNews[0].NewsPublishedDate;
        }

        public async Task<List<NewsInsertedsCmd>> GetManagements()
        {
            var managmentes = await _context.NewsInsertedsCmds.ToListAsync();
            return managmentes;
        }

        public async Task<string> GetManagementsTitle(long managementId)
        {
            var management = await _context.NewsInsertedsCmds.SingleAsync(s => s.Id == managementId);
            return management.Title;
        }

        public async Task<List<NegativeReason>> GetNegativeReasons()
        {
            return await _context.NegativeReasons.ToListAsync();
        }

        public async Task<List<NegativeOilDto>> GetNegativeOilNewsForSendingSms()
        {
            var list = await _context.NegativeOilNewsForSendingSms.ToListAsync();
            List<NegativeOilDto> dtos = new List<NegativeOilDto>();
            foreach (var oil in list)
            {
                var news = await _context.NewsRssReeds.SingleAsync(n => n.Id == oil.NewsId);
                NegativeOilDto dto = new NegativeOilDto()
                {
                    Id = oil.Id,
                    NewsId = news.Id,
                    Description = news.Description,
                    Title = news.Title
                };
                dtos.Add(dto);
            }
            return dtos;
        }

        public async Task<List<NotOilReason>> GetNotOilReasons()
        {
            return await _context.NotOilReasons.ToListAsync();
        }

        public async Task<OilNewsDto> GetOilNews(long newsId)
        {
            var news = await _context.NewsRssReeds.SingleOrDefaultAsync(n => n.Id == newsId);
            OilNewsDto dto = new OilNewsDto();
            if (news != null)
            {
                dto.Id = news.Id;
                dto.Title = news.Title;
                dto.Description = news.Description;
                dto.LinkUrl = news.LinkUrl;
                dto.Enclosure = news.Enclosure;
                dto.PublishedDate = news.PublishedDate;
                dto.Author = news.Author;
                dto.Category = news.Category;
            }
            return dto;
        }

        public async Task<List<NewsRelevance>> GetRelevances()
        {
            return await _context.NewsRelevances.ToListAsync();
        }

        public async Task<List<StatisticalSource>> GetStatisticalSource()
        {
            return await _context.StatisticalSources.ToListAsync();
        }

        public async Task<bool> IsEvaluated(long newsId)
        {
            return await _context.EvaluatedResults.AnyAsync(n => n.NewsId == newsId);
        }

        public async Task NotOilNewsWithReason(long newsId, int reasonId, long userId)
        {
            var news = await _context.NewsRssReeds.SingleAsync(n => n.Id == newsId);
            news.NotOil = reasonId;
            news.UserIdThatMakeIdNotOil = userId;
            _context.NewsRssReeds.Update(news);
            await _context.SaveChangesAsync();
        }

        public async Task SendSms(string mobile, string message)
        {
            Api sms = new Api("AiZrUW5JyMa5WHtEd+xkA21v9T30T6Rm3IgwE00TEkY");
            //var res = await sms.SendSMSAsync(message, mobile, "30006708101010", DateTime.Now);
            //send sms 
            var res = await sms.SendSMSAsync(message, mobile, "30006708101010", DateTime.Now, "12345");
            //send sms bulk
            var result = res.Result;
        }

        public async Task<List<OilNewsListDto>> ShowOilNewsByMainKey(long mainId)
        {
            List<OilNewsListDto> list = new List<OilNewsListDto>();
            var oils = await _context.NewsRssReedProcs.Where(n => n.KeyWordsMainId == mainId).ToListAsync();
            foreach (var oil in oils)
            {
                var news = await _context.NewsRssReeds.SingleAsync(n => n.Id == oil.NewsRssReedId);
                var site = news.LinkUrl.Split("/");
                OilNewsListDto od = new OilNewsListDto()
                {
                    Description = news.Description,
                    Id = news.Id,
                    Title = news.Title,
                    LinkUrl = news.LinkUrl,
                    TitleUrl = site[2]
                };
                list.Add(od);

            }
            return list;
        }

        public async Task AddToNegativeOilNewsForSend(long newsId, long userId, int typeId)
        {

            NegativeOilNewsForSendingSms negative = new NegativeOilNewsForSendingSms()
            {
                NewsId = newsId,
                TypeId = typeId,
                CreatedUserId = GetIpAddress.GetIp(),
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
            await _context.NegativeOilNewsForSendingSms.AddAsync(negative);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SendingSms()
        {
            try
            {
                var sendings = await _context.SendSms.ToListAsync();
                sendings = sendings.Where(s => s.IsSended == false).ToList();
                foreach (var sending in sendings)
                {
                    await SendSms(sending.Mobile, sending.SendedText);
                    sending.IsSended = true;
                    sending.SendDate = DateTime.Now;
                    _context.SendSms.Update(sending);
                    await _context.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }

        public async Task<List<SendingSmsType>> GetSendingSmsTypes()
        {
          return await _context.SendingSmsTypes.ToListAsync();
        }
    }
}
