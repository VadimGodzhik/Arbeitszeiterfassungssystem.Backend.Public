using System.Collections.Generic;
using BRIT.Models.Entities;
using BRIT.Models.ViewModels;
//using BRIT.Models.Entities.Owned;


namespace BRIT.Dal.Initialization
{
    public static class SampleData
    {
        public static List<Angestellte> Angestelltes => new()
        {
            new() {Id = 1, Vorname = "Dave", Nachname = "Brenner"},
            new() {Id = 1, Vorname = "Matt", Nachname = "Walton"},
            new() {Id = 1, Vorname = "Steve", Nachname = "Hagen"},
            new() {Id = 1, Vorname = "Pat", Nachname = "Walton"},
            new() {Id = 1, Vorname = "Bad", Nachname = "Customer"},
        };

        public static List<Arbeitsort> Arbeitsorte => new()
        {
            new() {Id = 1, Ort = "Münster", Postleitzahl = "48155", Straße ="Münsterstraße", Hausnummer = "111"}
        };

        public static List<AngestellteArbeitsortViewModel> AngestellteArbeitsortViewModels => new()
        {
            new() { AngestellteId = Angestelltes[0].Id, IstAngestellt = Angestelltes[0].IstAngestellt, Vorname = Angestelltes[0].Vorname, Nachname = Angestelltes[0].Nachname, ArbeitsortId = Arbeitsorte[0].Id, Ort = Arbeitsorte[0].Ort, Postleitzahl = Arbeitsorte[0].Postleitzahl, Straße = Arbeitsorte[0].Straße},
            new() { AngestellteId = Angestelltes[1].Id, IstAngestellt = Angestelltes[1].IstAngestellt, Vorname = Angestelltes[1].Vorname, Nachname = Angestelltes[1].Nachname, ArbeitsortId = Arbeitsorte[0].Id, Ort = Arbeitsorte[0].Ort, Postleitzahl = Arbeitsorte[0].Postleitzahl, Straße = Arbeitsorte[0].Straße},
            new() { AngestellteId = Angestelltes[2].Id, IstAngestellt = Angestelltes[2].IstAngestellt, Vorname = Angestelltes[2].Vorname, Nachname = Angestelltes[1].Nachname, ArbeitsortId = Arbeitsorte[0].Id, Ort = Arbeitsorte[0].Ort, Postleitzahl = Arbeitsorte[0].Postleitzahl, Straße = Arbeitsorte[0].Straße}

    };
    }
}
