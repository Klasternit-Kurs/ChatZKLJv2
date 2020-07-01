using BazeZklj.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BazeZklj.Server
{
	public class Hab : Hub
	{
		public void KaServeru(Osoba o)
		{
			Baza db = new Baza();
			Console.WriteLine($"{o.ID} - {o.Ime} - {o.Prezime}");
			var oso = db.Osobas.Find(o.ID);
			if (oso == null)
			{
				db.Osobas.Add(o);
			} else
			{
				db.Osobas.Remove(oso);
				db.Osobas.Add(o);
			}
			db.SaveChanges();
			PosaljiPodatke();
		}

		public void Obrisi(Osoba o)
		{
			Baza db = new Baza();
			db.Osobas.Remove(o);
			db.SaveChanges();
			PosaljiPodatke();
		}

		public void PosaljiPodatke()
		{
			Baza db = new Baza();
			Clients.All.SendAsync("KaKlijentu", db.Osobas.ToList());
		}
	}
}
