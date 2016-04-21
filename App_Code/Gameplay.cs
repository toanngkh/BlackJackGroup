using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Gameplay contains game logic and operations such as dealing cards, calculating scores
/// </summary>
public class Gameplay
{
    private List<CardInfo> deck = new List<CardInfo>();
    private List<CardInfo> dealerHand;   // create dealer's hand
    private List<CardInfo> playerHand;   // create player's hand
    private int dealerScore;
    private int playerScore;
    private int playerWins;
    private int playerLosses;
    private int playerDraws;
    private int playerBlackJack;
    private int playerHScore;

	public Gameplay()
	{
        dealerScore = 0;
        playerScore = 0;
        dealerHand = new List<CardInfo>();
        playerHand = new List<CardInfo>();
        CreateDeck();
        // deal 2 starting cards into dealer's hand and player's hand
        for (int i = 0; i < 2; i++)
        {
            DealCard(dealerHand);
            DealCard(playerHand);
        }
	}

    private void CreateDeck()    // create a new deck by pulling all cards from cardinfo table
    {
        using (var context = new BlackJackOnlineEntities())
        {
            deck = (from c in context.CardInfoes
                    select c).ToList();
        }
    }

    public void DealCard(List<CardInfo> hand)   // deal a random card into a hand
    {
        Random rand = new Random();
        bool exist = false;
        var index = rand.Next(0, deck.Count);   //select a random number as index
        var randomCard = deck[index];   //select a random card by selecting the random index in deck of cards
        foreach (var c in hand)
        {
            if (c.CardInfoId == randomCard.CardInfoId)  //if that card is already in the hand, choose again
            {
                exist = true;
                break;
            }
        }
        if (!exist) //if that card is not already in the hand, add that card into hand
        {
            hand.Add(randomCard);
        }
    }

    private void getPlayerStat(User user){
        using (var context = new BlackJackOnlineEntities())
        {
            var stats = from u in context.Users
                        where u.user_id == 1
                        select new {u.wins, u.losses, u.draws, u.blackjack, u.highscore};
            foreach (var s in stats)
            {
                playerWins = Convert.ToInt32(s.wins);
                playerLosses = Convert.ToInt32(s.losses);
                playerDraws = Convert.ToInt32(s.draws);
                playerBlackJack = Convert.ToInt32(s.blackjack);
                playerHScore = Convert.ToInt32(s.highscore);
            }
        }
    }

    private int CalculateHandValue(List<CardInfo> hand) // calculate value of a hand
    {
        int value = 0;
        int ace = 0;
        foreach (var card in hand)
        {
            if (card.PokerOrder == 1)
                ace++;
            if (card.PokerOrder == 11 || card.PokerOrder == 12 || card.PokerOrder == 13)
                value += 10;
            else
                value += Convert.ToInt32(card.PokerOrder);
        }
        if(ace == 0)
            return value;
        else
        {
            if (value + 10 <= 21)
                value += 10;
        }
        return value;
    }
}