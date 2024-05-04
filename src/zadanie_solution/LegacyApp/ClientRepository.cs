using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp
{
    public class ClientRepository : IClientRepository
    {
        /// <summary>
        /// This collection is used to simulate remote database
        /// </summary>
        public static readonly Dictionary<int, Client> Database = new Dictionary<int, Client>()
        {
            {1, new Client{ClientId = 1, LastName = "Kowalski", Address = "Warszawa, Złota 12", Email = "kowalski@wp.pl", Type = ClientType.Normal}},
            {2, new Client{ClientId = 2, LastName = "Malewski", Address = "Warszawa, Koszykowa 86", Email = "malewski@gmail.pl", Type = ClientType.VeryImportant}},
            {3, new Client{ClientId = 3, LastName = "Smith", Address = "Warszawa, Kolorowa 22", Email = "smith@gmail.pl", Type = ClientType.Important}},
            {4, new Client{ClientId = 4, LastName = "Doe", Address = "Warszawa, Koszykowa 32", Email = "doe@gmail.pl", Type = ClientType.Important}},
            {5, new Client{ClientId = 5, LastName = "Kwiatkowski", Address = "Warszawa, Złota 52", Email = "kwiatkowski@wp.pl", Type = ClientType.Normal}},
            {6, new Client{ClientId = 6, LastName = "Andrzejewicz", Address = "Warszawa, Koszykowa 52", Email = "andrzejewicz@wp.pl", Type = ClientType.Normal}}
        };
        
        public ClientRepository()
        {
        }

        /// <summary>
        /// Simulating fetching a client from remote database
        /// </summary>
        /// <returns>Returning client object</returns>
        public Client GetById(int clientId)
        {
            int randomWaitTime = new Random().Next(2000);
            Thread.Sleep(randomWaitTime);

            if (Database.ContainsKey(clientId))
                return Database[clientId];

            throw new ArgumentException($"User with id {clientId} does not exist in database");
        }
    }
}