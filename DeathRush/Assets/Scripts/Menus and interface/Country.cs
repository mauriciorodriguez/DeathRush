using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Country : MonoBehaviour
{

    public enum NamesType
    {
        None,
        TheLeagueOfClans,
        SonsOfTheApocalypse,
        TheNewEmpire,
        SouthernFighters,
        TheWatchers,
        NuclearRepublic
    }

    public static Dictionary<NamesType, Sprite> flagSprite;
    public static Dictionary<NamesType, Dictionary<RacerData.Gender, List<string>>> countriesNames;

    /*public static Dictionary<NamesType, Sprite> flagSprite = new Dictionary<NamesType, Sprite>()
            {
                {NamesType.TheLeagueOfClans, Resources.Load<Sprite>(K.PATH_FLAG_LOC) },
                {NamesType.SonsOfTheApocalypse, Resources.Load<Sprite>(K.PATH_FLAG_SOTA) },
                {NamesType.TheNewEmpire, Resources.Load<Sprite>(K.PATH_FLAG_TNE) },
                {NamesType.SouthernFighters, Resources.Load<Sprite>(K.PATH_FLAG_SF) },
                {NamesType.TheWatchers, Resources.Load<Sprite>(K.PATH_FLAG_TW) },
                {NamesType.NuclearRepublic, Resources.Load<Sprite>(K.PATH_FLAG_NR) }
            };

    public static Dictionary<NamesType, Dictionary<RacerData.Gender, List<string>>> countriesNames = new Dictionary<NamesType, Dictionary<RacerData.Gender, List<string>>>()
    {
        {NamesType.TheLeagueOfClans, new Dictionary<RacerData.Gender, List<string>>()
            {
                {RacerData.Gender.Male, new List<string>
                    {
                        "Gyasi", "Moyo", "Haji", "Hakim", "Hamadi", "Hanif", "Haoniyao", "Iben", "Imasu", "Kabonero", "Kito", "Mazi", "Oban", "Nassor",
                        "Magomu", "Lema", "Zane", "Zain", "Taj", "Shaka", "Ade", "Salim", "Jabari", "Abiola", "Amarey", "Davu", "Kendi", "Odion"
                    }
                },
                {RacerData.Gender.Female, new List<string>
                    {
                        "Zoya", "Ode", "Ada", "Asha", "Imani", "Kali", "Lulu", "Uma", "Ami", "Maha", "Oni", "Sabra", "Tale", "Alika"
                    }
                }
            }
        },
        {NamesType.NuclearRepublic, new Dictionary<RacerData.Gender, List<string>>()
            {
                {RacerData.Gender.Male, new List<string>
                    {
                        "Aiguo", "Aki", "Bae", "Chin", "Cho", "Botan", "Chung", "Dai", "Daisuke", "Feng", "Hong", "Isamu", "Hisoka", "Horo", "Isama", "Jin",
                        "Ryuu", "Zian"
                    }
                },
                {RacerData.Gender.Female, new List<string>
                    {
                        "Sakura", "Shino", "Sato", "Shresth", "Sudarshini", "Taka", "Tokiko", "Usagi", "Vanida", "Washi", "Xiaoli", "Zhu"
                    }
                }
            }
        },
        {NamesType.TheNewEmpire, new Dictionary<RacerData.Gender, List<string>>()
            {
                {RacerData.Gender.Male, new List<string>
                    {
                        "Oliver", "Hugo", "Natan", "Leo", "Arthur", "Ben", "Paul", "Louis", "Felix", "Lorenzo", "Levi", "Jayden", "Harry", "Thomas",
                        "Logan", "William", "Magnus"
                    }
                },
                {RacerData.Gender.Female, new List<string>
                    {
                        "Aurora", "Abella", "Irene", "Ada", "Allyson", "Sara", "Alva", "Daria", "Leonie", "Johanna", "Jade"
                    }
                }
            }
        },
        {NamesType.TheWatchers, new Dictionary<RacerData.Gender, List<string>>()
            {
                {RacerData.Gender.Male, new List<string>
                    {
                        "Noah", "Micheal", "Jackson", "Jacob", "Joseph", "Walter", "Rex", "David", "John", "Lucky", "Henry", "Isaac", "Owen", "Nathan",
                        "Caleb", "Jack", "Jason", "Bruce", "Alfred", "Junior", "Craig", "Stan", "Molotov", "Arnold", "Raimundo", "Jackie", "Jay", "Dave"
                    }
                },
                {RacerData.Gender.Female, new List<string>
                    {
                        "Emma", "Olivia", "Sophia", "Mia", "Emily", "Amelia", "Charlotte", "Lily", "Evelyn", "Zoey", "Natalie", "Hannah", "Alexa",
                        "Leah", "Anna", "Sadie"
                    }
                }
            }
        },
        {NamesType.SouthernFighters, new Dictionary<RacerData.Gender, List<string>>()
            {
                {RacerData.Gender.Male, new List<string>
                    {
                        "Alejandro", "Ezequiel", "Cristian", "Mauricio", "Martin", "Marcos", "Pablo", "Julian", "Gabriel", "Samuel", "Gonzalo",
                        "Bruno", "Juan", "José", "Mateo", "Bautista", "Santiago", "Felipe", "Joaquin"
                    }
                },
                {RacerData.Gender.Female, new List<string>
                    {
                        "Camila", "Maria", "Laura", "Daniela", "Natalia", "Andrea", "Sofía", "Julia", "Mariana", "Rocio", "Aguistina", "Carla",
                        "Luisa", "Florencia", "Julieta", "Sandra"
                    }
                }
            }
        },
        {NamesType.SonsOfTheApocalypse, new Dictionary<RacerData.Gender, List<string>>()
            {
                {RacerData.Gender.Male, new List<string>
                    {
                        "Alexei", "Anton", "Boris", "Vadim", "Viktor", "Vladimir", "Gennady", "Grigory", "Dimitry", "Igor", "Yuri", "Maxim", "Narek", "Tavit"
                    }
                },
                {RacerData.Gender.Female, new List<string>
                    {
                        "Anastasia", "Alyona", "Esfir", "Gala", "Galina", "Inna", "Karina", "Katya", "Klava", "Luba", "Masha", "Natasha", "Yesfir"
                    }
                }
            }
        }
    };*/

    public string countryName;
    public Sprite flag;
    public int chaosLevel;
    public NamesType countryNameEnum;
    public ScreenCountrySelect screenCountrySelect;

    private void Awake()
    {
        if (flagSprite == null)
        {
            flagSprite = new Dictionary<NamesType, Sprite>()
            {
                {NamesType.TheLeagueOfClans, Resources.Load<Sprite>(K.PATH_FLAG_LOC) },
                {NamesType.SonsOfTheApocalypse, Resources.Load<Sprite>(K.PATH_FLAG_SOTA) },
                {NamesType.TheNewEmpire, Resources.Load<Sprite>(K.PATH_FLAG_TNE) },
                {NamesType.SouthernFighters, Resources.Load<Sprite>(K.PATH_FLAG_SF) },
                {NamesType.TheWatchers, Resources.Load<Sprite>(K.PATH_FLAG_TW) },
                {NamesType.NuclearRepublic, Resources.Load<Sprite>(K.PATH_FLAG_NR) }
            };
        }
        if (countriesNames == null)
        {
            countriesNames = new Dictionary<NamesType, Dictionary<RacerData.Gender, List<string>>>()
            {
                {NamesType.TheLeagueOfClans, new Dictionary<RacerData.Gender, List<string>>()
                    {
                        {RacerData.Gender.Male, new List<string>
                            {
                                "Gyasi", "Moyo", "Haji", "Hakim", "Hamadi", "Hanif", "Haoniyao", "Iben", "Imasu", "Kabonero", "Kito", "Mazi", "Oban", "Nassor",
                                "Magomu", "Lema", "Zane", "Zain", "Taj", "Shaka", "Ade", "Salim", "Jabari", "Abiola", "Amarey", "Davu", "Kendi", "Odion"
                            }
                        },
                        {RacerData.Gender.Female, new List<string>
                            {
                                "Zoya", "Ode", "Ada", "Asha", "Imani", "Kali", "Lulu", "Uma", "Ami", "Maha", "Oni", "Sabra", "Tale", "Alika"
                            }
                        }
                    }
                },
                {NamesType.NuclearRepublic, new Dictionary<RacerData.Gender, List<string>>()
                    {
                        {RacerData.Gender.Male, new List<string>
                            {
                                "Aiguo", "Aki", "Bae", "Chin", "Cho", "Botan", "Chung", "Dai", "Daisuke", "Feng", "Hong", "Isamu", "Hisoka", "Horo", "Isama", "Jin",
                                "Ryuu", "Zian"
                            }
                        },
                        {RacerData.Gender.Female, new List<string>
                            {
                                "Sakura", "Shino", "Sato", "Shresth", "Sudarshini", "Taka", "Tokiko", "Usagi", "Vanida", "Washi", "Xiaoli", "Zhu"
                            }
                        }
                    }
                },
                {NamesType.TheNewEmpire, new Dictionary<RacerData.Gender, List<string>>()
                    {
                        {RacerData.Gender.Male, new List<string>
                            {
                                "Oliver", "Hugo", "Natan", "Leo", "Arthur", "Ben", "Paul", "Louis", "Felix", "Lorenzo", "Levi", "Jayden", "Harry", "Thomas",
                                "Logan", "William", "Magnus"
                            }
                        },
                        {RacerData.Gender.Female, new List<string>
                            {
                                "Aurora", "Abella", "Irene", "Ada", "Allyson", "Sara", "Alva", "Daria", "Leonie", "Johanna", "Jade"
                            }
                        }
                    }
                },
                {NamesType.TheWatchers, new Dictionary<RacerData.Gender, List<string>>()
                    {
                        {RacerData.Gender.Male, new List<string>
                            {
                                "Noah", "Micheal", "Jackson", "Jacob", "Joseph", "Walter", "Rex", "David", "John", "Lucky", "Henry", "Isaac", "Owen", "Nathan",
                                "Caleb", "Jack", "Jason", "Bruce", "Alfred", "Junior", "Craig", "Stan", "Molotov", "Arnold", "Raimundo", "Jackie", "Jay", "Dave"
                            }
                        },
                        {RacerData.Gender.Female, new List<string>
                            {
                                "Emma", "Olivia", "Sophia", "Mia", "Emily", "Amelia", "Charlotte", "Lily", "Evelyn", "Zoey", "Natalie", "Hannah", "Alexa",
                                "Leah", "Anna", "Sadie"
                            }
                        }
                    }
                },
                {NamesType.SouthernFighters, new Dictionary<RacerData.Gender, List<string>>()
                    {
                        {RacerData.Gender.Male, new List<string>
                            {
                                "Alejandro", "Ezequiel", "Cristian", "Mauricio", "Martin", "Marcos", "Pablo", "Julian", "Gabriel", "Samuel", "Gonzalo",
                                "Bruno", "Juan", "José", "Mateo", "Bautista", "Santiago", "Felipe", "Joaquin"
                            }
                        },
                        {RacerData.Gender.Female, new List<string>
                            {
                                "Camila", "Maria", "Laura", "Daniela", "Natalia", "Andrea", "Sofía", "Julia", "Mariana", "Rocio", "Aguistina", "Carla",
                                "Luisa", "Florencia", "Julieta", "Sandra"
                            }
                        }
                    }
                },
                {NamesType.SonsOfTheApocalypse, new Dictionary<RacerData.Gender, List<string>>()
                    {
                        {RacerData.Gender.Male, new List<string>
                            {
                                "Alexei", "Anton", "Boris", "Vadim", "Viktor", "Vladimir", "Gennady", "Grigory", "Dimitry", "Igor", "Yuri", "Maxim", "Narek", "Tavit"
                            }
                        },
                        {RacerData.Gender.Female, new List<string>
                            {
                                "Anastasia", "Alyona", "Esfir", "Gala", "Galina", "Inna", "Karina", "Katya", "Klava", "Luba", "Masha", "Natasha", "Yesfir"
                            }
                        }
                    }
                }
            };
        }
    }

    void OnMouseDown()
    {
        if (screenCountrySelect != null)
        {
            screenCountrySelect.OnSelectCountryButton(this);
        }
    }
}
