using ReadAndAnalysis.App.DTOs.News;
using ReadAndAnalysis.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Services.Interfaces
{
    public interface INewsService
    {
        Task<List<OilNewsListDto>> GetAllOilNews(string? starDate,string? endDate);
        Task<List<MainKeyWordListDto>> GetAllMainKeyWords();
        Task<KeyWordsUsedInOilNewsDto> GetAllUsedKeyWords(long newsId);
        Task<OilNewsDto> GetOilNews(long newsId);
        Task<bool> AddMainKeyWord(string title, long userId);
        Task<List<OilNewsListDto>> ShowOilNewsByMainKey(long mainId);
        Task<List<EstimateNews>> GetEstimateNews();
        Task<List<FieldOfUse>> GetFieldOfUse();
        Task<List<StatisticalSource>> GetStatisticalSource();
        Task<List<FieldOfUse>> GetChildFieldOfUse(long parent);
        Task<bool> IsEvaluated(long newsId);
        Task<EvaluatedResult?> GetEvaluatedResult(long newsId);
        Task DeleteEvaluated(long newsId);
        Task<string?> GetEvalutedFieldTitle(long newsId);
        Task<bool> CanAddEvalutedResult(long newsId);
        Task AddEvalutedResult(long newsId, long fieldchild,
            long? source, long estimate, long userId,long? relevance);
        Task<List<EvalutedListDto>> GetEvalutedNews(string? startDate, string? endDate, long? estimateId);
        Task<List<InsertedNewsListDto>> GetAllInsertedNews(string? startDate,string? endDate,int? estimateId );
        Task AddInsertedsNews(CreateInsertedNewsDto create, long userId);
        Task<InsertedNewsListDto> GetInsertedById(long id);
        Task EditInsertedNews(InsertedNewsListDto  insert, long userId);
        Task<List<NewsInsertedsCmd>> GetManagements();
        Task<string> GetManagementsTitle(long managementId);
        Task<string?> GetLastPublishedDate(long userId);
        Task<List<NegativeReason>> GetNegativeReasons();
        Task<List<NewsRelevance>> GetRelevances();
        Task DeleteOilNews(long newsId , long userId);
        Task<List<NotOilReason>> GetNotOilReasons();
        Task NotOilNewsWithReason(long newsId, int reasonId, long userId);
        Task SendSms(string mobile, string message);
        Task<List<NegativeOilDto>> GetNegativeOilNewsForSendingSms();
        Task AddToNegativeOilNewsForSend(long newsId, long userId,int typeId);
        Task<bool> SendingSms();
        Task<List<SendingSmsType>> GetSendingSmsTypes();
        Task<List<SelectedNewsDto>> GetSelectedNewsTypes();
        Task SendNextLevel(long negativeSmsId,long userId);
        Task ViewWithNoAction(long negativeSmsId, long userId);
        Task SendNextLevelWithUrl(string key);
        Task<OilNewsDto> GetNewsByKey(string key);
        Task<bool> IsNewsSendedSms(string  key);
        Task CreateNewsAsPrivateMode(CreateNewsAsPrivateDto create, long userId);
        Task<List<OilNewsDto>> GetLikeNews(long newsId);
        Task UpdateLikeNews(long newsId, List<long>likesIds, long userId);
    }
}
