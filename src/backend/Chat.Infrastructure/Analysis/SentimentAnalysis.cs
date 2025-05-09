using Azure.AI.TextAnalytics;
using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;

namespace Chat.Infrastructure.Analysis;

public class SentimentAnalysis : ISentimentAnalysis
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TextAnalyticsClient _textAnalyticsClient;

    public SentimentAnalysis(
        IUnitOfWork unitOfWork,
        TextAnalyticsClient textAnalyticsClient)
    {
        _unitOfWork = unitOfWork;
        _textAnalyticsClient = textAnalyticsClient;
    }

    public async Task<string> AnalyzeSentimentAsync(SentimentDto sentimentDto)
    {
        var analysisResult = await _textAnalyticsClient.AnalyzeSentimentAsync(sentimentDto.Content);

        var sentiment = new Sentiment
        {
            MessageId = sentimentDto.MessageId,
            SentimentResult = analysisResult.Value.Sentiment.ToString(),
            PositiveScore = analysisResult.Value.ConfidenceScores.Positive,
            NeutralScore = analysisResult.Value.ConfidenceScores.Neutral,
            NegativeScore = analysisResult.Value.ConfidenceScores.Negative
        };

        await _unitOfWork.SentimentRepository.CreateAsync(sentiment);
        await _unitOfWork.SaveAsync();

        return sentiment.SentimentResult;
    }
}