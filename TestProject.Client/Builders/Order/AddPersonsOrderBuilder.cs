using System.Linq.Expressions;
using Bogus;
using TestProject.Client.Models.Order;

namespace TestProject.Client.Builders.Order;

public class AddGuestsOrderBuilder
{
    private readonly RequestCalculateOrderTotalModel _request;

    private AddGuestsOrderBuilder()
    {
        this._request = new RequestCalculateOrderTotalModel
        {
            GuestsOrderModels = new List<GuestsOrderModel>()
        };
        this._request = this.CreateDefaultModel();
    }
    
    public AddGuestsOrderBuilder AddOrdersList(List<GuestsOrderModel> guestsOrders)
    {
        //removing default model
        this._request.GuestsOrderModels.RemoveAt(0);
        
        this._request.GuestsOrderModels.AddRange(guestsOrders);
        return this;
    }

    public AddGuestsOrderBuilder AddGuestsOrder()
    {
        this._request.GuestsOrderModels.Add(new GuestsOrderModel());
        return this;
    }

    public AddGuestsOrderBuilder WithMainsCount(int mainsCount)
    {
        this._request.GuestsOrderModels.Last().MainsCount = mainsCount;
        return this;
    }

    public AddGuestsOrderBuilder WithStartersCount(int startersCount)
    {
        this._request.GuestsOrderModels.Last().StartersCount = startersCount;
        return this;
    }

    public AddGuestsOrderBuilder WithDrinksWithDiscount(int drinksWithDiscountCount)
    {
        this._request.GuestsOrderModels.Last().DrinksWithDiscountCount = drinksWithDiscountCount;
        return this;
    }

    public AddGuestsOrderBuilder WithDrinksWithoutDiscount(int drinksWithoutDiscountCount)
    {
        this._request.GuestsOrderModels.Last().DrinksWithoutDiscountCount = drinksWithoutDiscountCount;
        return this;
    }
    
    public static AddGuestsOrderBuilder CreateNew() => new ();

    public RequestCalculateOrderTotalModel Build() => this._request;

    private RequestCalculateOrderTotalModel CreateDefaultModel()
    {
        var rnd = new Random();
        this._request.GuestsOrderModels.Add(new Faker<GuestsOrderModel>()

            .RuleFor<int>((Expression<Func<GuestsOrderModel, int>>)(o => o.MainsCount), rnd.Next(1, 4))
            .RuleFor<int>((Expression<Func<GuestsOrderModel, int>>)(o => o.StartersCount), rnd.Next(0, 4))
            .RuleFor<int>((Expression<Func<GuestsOrderModel, int>>)(o => o.DrinksWithDiscountCount), rnd.Next(0, 4))
            .RuleFor<int>((Expression<Func<GuestsOrderModel, int>>)(o => o.DrinksWithoutDiscountCount), rnd.Next(0, 4))
            .Generate());
        return this._request;
    }
}