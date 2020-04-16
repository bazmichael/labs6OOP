using System;
using System.Collections.Generic;


namespace Lab06
{
    class Program
    {
        class Dyrekcja
        {
            private List<Klatka> klatki = new List<Klatka>();
            private List<Opiekun> opiekunowie = new List<Opiekun>();
            private List<Zwierze> rejestrZwierzat = new List<Zwierze>();

            public void ZatrudnicOpiekuna(Opiekun opiekun) 
            {
                opiekunowie.Add(opiekun);
            }

            public bool ZwolnicOpiekuna(Opiekun opiekun)
            {
                bool success = false;
                Opiekun tempOpiekun = opiekunowie.Find(x => x.Equals(opiekun));
                if (tempOpiekun != null)
                {
                    opiekunowie.Remove(opiekun);
                    success = true;
                }
                return success;
            }


            public bool PrypiszKlatkeOpiekunowi(string imie, string nazwisko, int idKlatki)
            {
                bool success = false;

                Klatka tempKlatka = klatki.Find(x => x.IdKlatki == idKlatki); 
                Opiekun tempOpiekun = opiekunowie.Find(x => x.Imie == imie && x.Nazwisko == nazwisko);
                if (tempOpiekun != null)
                {
                    if(tempKlatka != null)
                    {
                        tempOpiekun.PrzydzielKlatke(tempKlatka);
                        success = true;
                    }
                    else
                        Console.WriteLine("[!!!] Klatka o podanym ID nie jest zarejestrowana w zoo");
                       
                    

                }

                else                     
                    Console.WriteLine("[!!!] Nie udalo sie odnalesc podanego opiekuna");
                    
                
                    return success;
            }

            public void ZbudujKlatke(Klatka klatka)
            {
                klatki.Add(klatka);
            }

            public void ZbudujKlatke(double pojemnosc)
            {
                Klatka dodawanaKlatka = new Klatka(pojemnosc);
                klatki.Add(dodawanaKlatka);
            }


            public bool PowiekszKlatke(int id, double nowaPojemnosc)
            {
                bool success = false;

                foreach (Klatka klatka in klatki)
                {
                    if (klatka.IdKlatki == id)
                    {
                        klatka.Pojemnosc = nowaPojemnosc;
                        success = true;
                    }
                }
                return success;
            }

            public bool PrzeniesZwierze(int idAktualnejKlatki, int idNowejKlatki, Zwierze zwierze)
            {
                bool success = false;
                bool wyszukanoWAktualnejKlatce = false;
                foreach(Klatka klatka in klatki)
                {
                    if (klatka.IdKlatki == idAktualnejKlatki) 
                    {
                        wyszukanoWAktualnejKlatce = klatka.WyszukajZwierze(zwierze);
                    }

                }

                if (wyszukanoWAktualnejKlatce)
                {
                    foreach (Klatka klatka in klatki)
                    {
                        if (klatka.IdKlatki == idAktualnejKlatki)
                            klatka.WypiszZwierze(zwierze);
                    }

                    PrzypiszZwierzeDoKlatki(idNowejKlatki, zwierze);
                    Console.WriteLine("\nZwierze zostalo przeniesione do klatki ID [" + idNowejKlatki + "].");
                    success = true;
                }
                else
                {
                    Console.WriteLine("\nPodane zwierze nie zarejestrowane w klatce ID [" + idAktualnejKlatki + "].");
                }
        
                return success;
            }

            public bool PrzypiszZwierzeDoWolnejKlatki(Zwierze zwierze)
            {
                bool success = false;
                foreach(Klatka klatka in klatki)
                {
                    if(klatka.IloscZwierzatWKlatce() == 0)
                    {
                        klatka.PrzypiszKlatke(zwierze);
                        rejestrZwierzat.Add(zwierze);
                        Console.WriteLine("\n[PRZYPISANIE] Zwierze zostalo przypisane do wolnej klatki o ID [" + klatka.IdKlatki + "].");
                        success = true;
                    }
                }
                

                return success;
            }

            public bool PrzypiszZwierzeDoKlatki(int IdKlatki, Zwierze zwierze)
            {
                bool success = false;
                foreach (Klatka klatka in klatki)
                {
                    if (klatka.IdKlatki == IdKlatki)
                    {
                        if (!klatka.WyszukajZwierze(zwierze))
                        {
                            klatka.PrzypiszKlatke(zwierze);
                            rejestrZwierzat.Add(zwierze);
                            success = true;
                        }
                    } 
                }

                return success;
                
            }

            public void InfoKlatki(bool infoZwierzeta, bool pokazCechy, bool pokazUmietnosci) 
            {
                foreach (Klatka klatka in klatki)
                    klatka.WypiszInfo(infoZwierzeta, pokazCechy, pokazUmietnosci);
            }


            public void InfoOpiekunowie(bool infoKlatki) {
                foreach (Opiekun opiekun in opiekunowie)
                    opiekun.WypiszInfo(infoKlatki);
            }

            public void InfoZwierzeta(bool infoCechy, bool infoUmietnosci)
            {
                foreach (Zwierze zwierze in rejestrZwierzat)
                    zwierze.WypiszInfo(infoCechy, infoUmietnosci);
            }

        }


        class Klatka {
            private double pojemnosc = 0;
            private static int liczbaKlatek = 0;
            private int idKlatki;
            private List<Zwierze> zwierzeta = new List<Zwierze>();
            private bool wymagaSprzatania = false;           
            public double Pojemnosc
            {
                get => pojemnosc;
                set => pojemnosc = value;
            }
            public int IdKlatki
            {
                get => idKlatki;
                set => idKlatki = value;
            }

            public bool WymagaSprzatania { set => wymagaSprzatania = value; }

            

            
            
            public Klatka()
            {
                pojemnosc = 0.0;
            }
            public Klatka(double pojemnosc_)
            {
                pojemnosc = pojemnosc_;
                liczbaKlatek++;
                idKlatki = liczbaKlatek;
            }

            public int IloscZwierzatWKlatce()
            {
                return zwierzeta.Count;
            }
            public void PrzypiszKlatke(Zwierze zwierze)
            {
                zwierzeta.Add(zwierze);

            }
            
            public void WypiszZwierze(Zwierze zwierze_)
            {
                zwierzeta.Remove(zwierze_);
            }

            public bool WyszukajZwierze(Zwierze zwierze) 
            {
                bool found = false;

                foreach(Zwierze szukaneZwierze in zwierzeta)
                {
                    if (szukaneZwierze.Gatunek == zwierze.Gatunek && szukaneZwierze.Pochodzenie == zwierze.Pochodzenie) 
                        found = true;
                    
                }

                return found;
            }
            public void WypiszInfo(bool pokazZwierzet, bool pokazCechy, bool pokazUmietnosci)
            {
                Console.WriteLine("\n[INFO O KLATCE ID: " + idKlatki + "] POJEMNOSC: " + pojemnosc + " [m3]. Wymaga sprzatania?: " + wymagaSprzatania);
                if (pokazZwierzet)
                {

                    if (zwierzeta.Count >= 1)
                    {

                        Console.WriteLine("[ZWIERZETA W KLATCE ID " + idKlatki + "]");
                        foreach (Zwierze zwierze in zwierzeta)
                        {
                            zwierze.WypiszInfo(pokazCechy, pokazUmietnosci);
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("W danej klatce nie zostalo przypisano zwierzat!");
                    }
                           
                    

                }
            }

        }

        abstract class Zwierze
        {
            protected string gatunek { get; } = " ";
            protected string rodzajPozywienia { get; } = " ";
            protected string pochodzenie { get; } = " ";
          
            protected List<string> cechy = new List<string>();
            protected List<string> umietnosci = new List<string>();

            public string Gatunek { get => gatunek; }

            public string Pochodzenie { get => pochodzenie; }
            public string RodzajPozywienia { get => rodzajPozywienia; }
           

           

            public Zwierze()
            {
                gatunek = "nieznany";
                rodzajPozywienia = "nieznany";
                pochodzenie = "nieznane";
         
            }

            public Zwierze(string gatunek_, string rodzajPozywienia_, string pochodzenie_)
            {
                gatunek = gatunek_;
                rodzajPozywienia = rodzajPozywienia_;
                pochodzenie = pochodzenie_;
             

            }


            public void DodajCeche(string cecha)
            {
                cechy.Add(cecha);
            }

            public void DodajUmietnosc(string umietnosc)
            {
                umietnosci.Add(umietnosc);
            }

          

            public void InfoCechy()
            {
                if (cechy.Count > 0)
                {
                    Console.WriteLine("CECHY:");
                    foreach (string cecha in cechy)
                        Console.WriteLine(cecha);
                }
                else
                {
                    Console.WriteLine("[!!!] NIE MA PRZYPISANYCH CECH DO ZWIERZATKA");
                }
            }

            public void InfoUmietnosci()
            {
                if (umietnosci.Count > 0)
                {
                    Console.WriteLine("UMIETNOSCI:");
                    foreach (string um in umietnosci)
                        Console.WriteLine(um);
                }
                else
                {
                    Console.WriteLine("[!!!] NIE MA PRZYPISANYCH UMIETNOSCI DO ZWIERZATKA");
                }
            }

            public virtual void WypiszInfo(bool InfoCechy_, bool InfoUmietnosci_)
            {
                Console.WriteLine("[INFO O  ZWIERZAKU] GATUNEK: " + gatunek + ". ROD. POZYWIENIA: " + rodzajPozywienia + ". POCHODZENIE: " + pochodzenie);
            }

        };

        class Ptak : Zwierze
        {
            private double rozpietoscSkrzydel = 0.0, wytrzymalosc = 0.0;

            public Ptak() : base()
            {
                rozpietoscSkrzydel = wytrzymalosc = 0.0;
            }

            public Ptak(int rozpietoscSkrzydel_, int wytzymalosc_, string gatunek, string rodzajPozywienia, string pochodzenie) : base(gatunek, rodzajPozywienia, pochodzenie)
            {
                rozpietoscSkrzydel = rozpietoscSkrzydel_;
                wytrzymalosc = wytzymalosc_;
            }

            public double MaksymalnaDlugoscLotu()
            {
                return rozpietoscSkrzydel * wytrzymalosc;
            }

            public override void WypiszInfo(bool InfoCechy_, bool InfoUmietnosci_)
            {
                Console.WriteLine("\n[INFO O PTAKU] ROZPIETOSCSKRZYDEL: " + rozpietoscSkrzydel + ". WYTRZYMALOSC: " + wytrzymalosc);
                Console.WriteLine("[INFO O PTAKU] GATUNEK: " + gatunek + ". RODZ.POZYWIENIA: " + rodzajPozywienia + ". POCHODZENIE: " + pochodzenie);
                if (InfoCechy_)
                    InfoCechy();
                if (InfoUmietnosci_)
                    InfoUmietnosci();
            }

        }




        class Ssak : Zwierze
        {
            private string naturalneSrodowisko = " ";

            public Ssak() : base()
            {
                naturalneSrodowisko = "nieznane";
            }

            public Ssak(string naturalneSrodowisko_, string gatunek, string rodzajPozywienia, string pochodzenie) : base(gatunek, rodzajPozywienia, pochodzenie)
            {
                naturalneSrodowisko = naturalneSrodowisko_;
            }


            public override void WypiszInfo(bool InfoCechy_, bool InfoUmietnosci_)
            {
                Console.WriteLine("\n[INFO O SSAKU] NATURALNE SRODOWISKO: " + naturalneSrodowisko);
                Console.WriteLine("[INFO O SSAKU] GATUNEK: " + gatunek + ". RODZ.POZYWIENIA: " + rodzajPozywienia + ". POCHODZENIE: " + pochodzenie);
                if (InfoCechy_)
                    InfoCechy();
                if (InfoUmietnosci_)
                    InfoUmietnosci();
            }

        }



        class Gad : Zwierze
        {
            private bool czyJadowity = false;
            public Gad() : base()
            {
                czyJadowity = false;
            }

            public Gad(bool czyJadowity_, string gatunek, string rodzajPozywienia, string pochodzenie) : base(gatunek, rodzajPozywienia, pochodzenie)
            {
                czyJadowity = czyJadowity_;
            }

            public override void WypiszInfo(bool InfoCechy_, bool InfoUmietnosci_)
            {
                if (czyJadowity)
                    Console.WriteLine("\n[INFO O GADZIE] JADOWITOSC: TAK");
                else
                    Console.WriteLine("\n[INFO O GADZIE] JADOWITOSC: NIE");
                Console.WriteLine("[INFO O GADZIE] GATUNEK: " + gatunek + ". RODZ.POZYWIENIA: " + rodzajPozywienia + ". POCHODZENIE: " + pochodzenie);
                if (InfoCechy_)
                    InfoCechy();
                if (InfoUmietnosci_)
                    InfoUmietnosci();
            }

        }


        class Osoba
        {
            private string nazwisko, imie;
            private string dataUrodzenia;

            public string Nazwisko { get { return nazwisko; } }
            public string Imie { get { return imie; } }

            public string  DataUrodzenia{ get { return dataUrodzenia; } }

            public Osoba(string nazwisko_, string imie_, string dataUrodzenia_)
            {
                nazwisko = nazwisko_;
                imie = imie_;
                dataUrodzenia = dataUrodzenia_;
            
            }
        }

        class Opiekun: Osoba
        {
            private List<Klatka> klatki = new List<Klatka>();
            public Opiekun(string nazwisko, string imie, string dataUrodzenia): base(nazwisko, imie, dataUrodzenia)
            {

            }

            public void PrzydzielKlatke(Klatka klatka)
            {
                klatki.Add(klatka);
            }
            
            public void SprzatajWszystkieKlatki()
            {
                if (klatki.Count >= 1)
                {
                    foreach (Klatka klatka in klatki)
                        klatka.WymagaSprzatania = false;

                    Console.WriteLine("Wszystkie przydzielione opiekunowi klatki zostaly posprzatane!");
                }
                else
                    Console.WriteLine("\nNie ma przypisanych klatek danemu opiekunowi");
            }

            public bool SprzatajKlatke(int idKlatki)
            {
                bool success = false;

                Klatka szukanaKlatka = klatki.Find(x => x.IdKlatki == idKlatki);
                if (szukanaKlatka != null)
                {
                    Console.WriteLine("\nKlatka zostala posprzatana!");
                    szukanaKlatka.WymagaSprzatania = false;
                    success = true;
                }
                else
                    Console.WriteLine("\nKlatka o podanym ID nie jest przypisana do danego opiekuna");
                    

                return success;
            }

            public bool SprzatajKlatke(Klatka klatka)
            {
                bool success = false;



                foreach(Klatka klatka1 in klatki)
                {
                    if (klatka.IdKlatki == klatka1.IdKlatki) {
                        klatka.WymagaSprzatania = false;
                        Console.WriteLine("\nPodana klatka zostala posprzatana!");
                        success = true;
                    }
                }
                
                if(!success)
                    Console.WriteLine("\nPodana klatka nie jest przypisana danemu opiekunowi!");

                return success;

            }

            public void WypiszInfo(bool infoKlatki)
            {
                Console.WriteLine("\n[INFO O OPIEKUNIE] " + Imie + " " + Nazwisko + ". Data urodzenia: " + DataUrodzenia) ;
                if (infoKlatki)
                {
                    if(klatki.Count >= 1)
                    {
                        Console.WriteLine("ODPOWIADA ZA NASTEPNE KLATKI: ");
                        foreach (Klatka klatka in klatki)
                        {
                            Console.WriteLine("#Klatka ID [" + klatka.IdKlatki + "]");
                        }
                    }
                    else
                    {
                        Console.WriteLine("NIE PRZYPISANO KLATEK DO OPIEKUNA");
                    }
                    
                }
            }
        }




        static void Main(string[] args)
        {
            Dyrekcja dyrekcja = new Dyrekcja();

            Opiekun[] opiekunowie = new Opiekun[6]
            {
                new Opiekun("Balcewicz", "Tomasz", "10.04.1986"),
                new Opiekun("Dabrowska", "Angela", "01.12.1990"),
                new Opiekun("Maslowski", "Bernard", "25.02.1967"),
                new Opiekun("Kot", "Arkadiusz", "14.05.1975"),
                new Opiekun("Brzezinski", "Mateusz", "11.11.1995"),
                new Opiekun("A", "B", "11.11.1111")
            };

            Ssak[] ssaki = new Ssak[5]
            {
                new Ssak("MROZNY OBSZAR", "ZABIELEK BIALAWY", "WSZYSTKOZERCY", "EUROPA WSCHODNIA"),
                new Ssak("GORACY TROPIK", "MOPEK", "WSZYSTKOZERCY", "AFRYKA"),
                new Ssak("MROZNY OBSZAR", "NOCEK", "ROSLINOZERCY", "POLNOCNA AMERYKA"),
                new Ssak("PODWODNE", "RYJOWKA AKSAMITNA", "WSZYSTKOZERCY", "EUROPA WSCHODNIA"),
                new Ssak("PODZIEMNE", "KRET EUROPEJSKI", "SAPROFAGI", "KRAJE AMERYKI POLUDNIOWEJ")
            };

            Ptak[] ptaki = new Ptak[5]
            {
                new Ptak(50, 40, "KOS", "WSZYSTKOZERCY", "POLNOCNA AMERYKA"),
                new Ptak(35, 120, "GRZYWACZ", "WSZYSTKOZERCY", "AFRYKA"),
                new Ptak(20, 250, "WROBEL", "WSZYSTKOZERCY", "EUROPA ZACHODNIA"),
                new Ptak(15, 35, "SZPAK", "WSZYSTKOZERCY", "AFRYKA"),
                new Ptak(40, 90, "SROKA", "WSZYSTKOZERCY", "EUROPA WSCHODNIA")
            };

            Gad[] gady = new Gad[5]
            {
                new Gad(true, "ZOLW", "ROSLINOZERCA", "AFRYKA"),
                new Gad(false, "JASZCZURKA", "MIESOZERCY", "KRAJE AMERYKI POLUDNIOWEJ"),
                new Gad(false, "PADALEC", "PASOZYTY", "EUROPA ZACHODNIA"),
                new Gad(true, "WAZ", "SAPROFAGI", "AFRYKA"),
                new Gad(false, "GNIEWOSZ", "ROSLINOZERCA", "POLNOCNA AMERYKA")
            };

        
            
            dyrekcja.ZbudujKlatke(100);
            dyrekcja.ZbudujKlatke(75);
            dyrekcja.ZbudujKlatke(50);
            dyrekcja.ZbudujKlatke(40);
            dyrekcja.ZbudujKlatke(120);


            dyrekcja.InfoKlatki(true, false, false); 

            dyrekcja.PrzypiszZwierzeDoKlatki(1, ptaki[0]);
            dyrekcja.PrzypiszZwierzeDoKlatki(1, ptaki[1]);
            dyrekcja.PrzypiszZwierzeDoKlatki(1, ptaki[2]);
            ptaki[0].DodajUmietnosc("Dzwiek ZZZZZZ");
            dyrekcja.PowiekszKlatke(1, 150);
            dyrekcja.InfoKlatki(true, false, true);

            dyrekcja.PrzypiszZwierzeDoKlatki(2, ssaki[0]);
            dyrekcja.PrzypiszZwierzeDoKlatki(2, ssaki[1]);

            dyrekcja.PrzypiszZwierzeDoWolnejKlatki(gady[0]);
            Console.WriteLine(dyrekcja.PrzypiszZwierzeDoWolnejKlatki(gady[1]));

            dyrekcja.InfoKlatki(true, false, false);

            dyrekcja.ZbudujKlatke(123);
            dyrekcja.PrzypiszZwierzeDoWolnejKlatki(ptaki[4]);
            dyrekcja.InfoKlatki(true, false, false);

            dyrekcja.PrzeniesZwierze(1, 1, ptaki[4]);
            dyrekcja.PrzeniesZwierze(6, 1, ptaki[4]);
            dyrekcja.InfoKlatki(true, false, false);

            dyrekcja.ZatrudnicOpiekuna(opiekunowie[0]);
            dyrekcja.ZatrudnicOpiekuna(opiekunowie[1]);
            dyrekcja.ZatrudnicOpiekuna(opiekunowie[2]);
            dyrekcja.ZatrudnicOpiekuna(opiekunowie[3]);
            dyrekcja.ZatrudnicOpiekuna(opiekunowie[4]);
            dyrekcja.ZatrudnicOpiekuna(opiekunowie[5]);

            dyrekcja.InfoOpiekunowie(true);

            dyrekcja.ZwolnicOpiekuna(opiekunowie[5]);
            dyrekcja.InfoOpiekunowie(false);

            dyrekcja.PrypiszKlatkeOpiekunowi("Tomasz", "Balcewicz", 4);
            dyrekcja.PrypiszKlatkeOpiekunowi("Tomasz", "Balcewicz", 3);
            dyrekcja.PrypiszKlatkeOpiekunowi("Angela", "Dabrowska", 2);
            dyrekcja.PrypiszKlatkeOpiekunowi("Tomasz", "Balcewicz", 11111);
            dyrekcja.PrypiszKlatkeOpiekunowi("A", "B", 3);

            dyrekcja.InfoOpiekunowie(true);

            opiekunowie[0].SprzatajKlatke(4);
            opiekunowie[0].SprzatajWszystkieKlatki();
            opiekunowie[4].SprzatajWszystkieKlatki();

            dyrekcja.InfoZwierzeta(false, false);

            Console.ReadKey(); 
        }
    }
}
