namespace EventLimiter
{
    internal sealed class CharacterEventsProvider
    {
        public List<string> Settings { get; } = new();

        private readonly Dictionary<string, Dictionary<string, Dictionary<int, string>>> _eventsData;

        public CharacterEventsProvider()
        {
            //_eventsData = Settings.Contains("FlashShifter.StardewValleyExpandedCP") ? InitializeEventsData_SVE() : InitializeEventsData();
            _eventsData = InitializeEventsData_SVE();

        }

        public Dictionary<int, string>? GetEventsDictForCharacter(string characterName)
        {
            if (!_eventsData.TryGetValue(characterName, out var variants))
                return null;

            switch (characterName)
            {
                case "Shane":
                    if (Settings.Contains("tenthousandcats.ImmersiveCShane"))
                        return variants.TryGetValue("immersive", out var dict) ? dict : variants["vanilla"];
                    break;
                case "Marnie":
                    if (Settings.Contains("Lemurkat.MarnieRanchPack.CP"))
                        return variants.TryGetValue("immersive", out var dict) ? dict : variants["vanilla"];
                    break;
                case "Jas":
                    if (Settings.Contains("Lemurkat.JasRanchPack.CP"))
                        return variants.TryGetValue("immersive", out var dict) ? dict : variants["vanilla"];
                    break;
                case "Clint":
                    if (Settings.Contains("no_emily"))
                        return variants.TryGetValue("no_emily", out var dict) ? dict : variants["vanilla"];
                    break;
            }

            return variants.TryGetValue("vanilla", out var defaultDict)
                ? defaultDict
                : null;
        }

        private Dictionary<string, Dictionary<string, Dictionary<int, string>>> InitializeEventsData()
        {
            return new()
            {
                ["Shane"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "611944"}, 
                        { 4, "3910674"}, 
                        { 6, "2118991"},
                        { 7, "3910974" }, 
                        { 8, "3900074"}, 
                        { 10, "9581348"}, 
                        { 14, "-1"}
                    },
                    ["immersive"] = new Dictionary<int, string>
                    {
                        { 2, "611944"}, 
                        { 4, "3910674"}, 
                        { 6, "2118991"},
                        { 7, "3910974" }, 
                        { 8, "9581348"}, 
                        { 10, "59443111"}, 
                        { 14, "-1"}
                    }
                },

                ["Alex"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "2481135"}, 
                        { 5, "21"}, 
                        { 6, "2119820"},
                        { 8, "288847"}, 
                        { 10, "911526"}, 
                        { 14, "-1"}
                    }
                },

                ["Sebastian"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "2794460"},
                        { 4, "384883"},
                        { 6, "27"},
                        { 8, "29"},
                        { 10, "384882"},
                        { 14, "-1"}
                    }
                },

                ["Sam"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "44"},
                        { 4, "46"},
                        { 6, "45"},
                        { 8, "4081148"},
                        { 10, "233104"},
                        { 14, "-1"}
                    }
                },

                ["Harvey"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "56"},
                        { 4, "57"},
                        { 6, "58"},
                        { 8, "571102"},
                        { 10, "528052"},
                        { 14, "-1"}
                    }
                },

                ["Elliott"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "39"},
                        { 4, "40"},
                        { 6, "423502"},
                        { 8, "1848481"},
                        { 10, "43"},
                        { 14, "-1"}
                    }
                },

                ["Abigail"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "1"},
                        { 4, "2"},
                        { 6, "4"},
                        { 8, "3"},
                        { 10, "901756"},
                        { 14, "-1"}
                    }
                },

                ["Leah"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "50"},
                        { 4, "51"},
                        { 6, "52"},
                        { 8, "55"},
                        { 10, "54"},
                        { 14, "-1"}
                    }
                },

                ["Maru"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "6"},
                        { 4, "7"},
                        { 6, "8"},
                        { 8, "9"},
                        { 10, "10"},
                        { 14, "-1"}
                    }
                },

                ["Penny"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "34"}, // (geroge wheelchair)
                        { 4, "35"},
                        { 6, "36"},
                        { 8, "181928"},
                        { 10, "38"},
                        { 14, "-1"}
                    }
                },

                ["Haley"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "11"},
                        { 4, "12"},
                        { 6, "13"},
                        { 8, "14"},
                        { 10, "15"},
                        { 14, "-1"}
                    }
                },

                ["Emily"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "471942"},
                        { 4, "463391"},
                        { 6, "917409"},
                        { 8, "2123243"},
                        { 10, "2123343"},
                        { 14, "-1"}
                    }
                },

                ["Willy"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "711130"},
                        { 10, "-1"},
                    }
                },

                ["Vincent"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 10, "-1"},
                    }
                },

                ["Wizard"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 10, "-1"},
                    }
                },

                ["Gus"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "96"},
                        { 5, "980558"},
                        { 10, "-1"},
                    }
                },

                ["Demetrius"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "25"},
                        { 10, "-1"},
                    }
                },

                ["Jas"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 10, "-1"},
                    },
                    ["immersive"] = new Dictionary<int, string>
                    {
                        { 2, "50706112"},
                        { 4, "50706113"},
                        { 7, "50706114"},
                        { 8, "50706115"},
                        { 10, "-1"},
                    }
                },

                ["Jodi"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "94"},
                        { 10, "-1"},
                    }
                },

                ["George"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "18"},
                        { 10, "-1"},
                    }
                },

                ["Kent"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "100"},
                        { 10, "-1"},
                    }
                },

                ["Clint"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 3, "97"},
                        { 6, "101"},
                        { 10, "-1"},
                    },
                    ["no_emily"] = new Dictionary<int, string>
                    {
                        { 3, "97"},
                        { 10, "-1"},
                    }
                },

                ["Krobus"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 10, "-1"},
                    }
                },

                ["Caroline"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "719926"},
                        { 6, "17"},
                        { 10, "-1"},
                    }
                },

                ["Leo"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "6497423"},
                        { 4, "6497421"},
                        { 6, "6497428"},
                        { 9, "8959199"},
                        { 10, "-1"},
                    }
                },

                ["Linus"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "26"},
                        { 8, "371652"},
                        { 10, "-1"},
                    }
                },

                ["Lewis"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "639373"},
                        { 10, "-1"},
                    }
                },

                ["Marnie"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 3, "92"},
                        { 6, "639373"},
                        { 10, "-1"},
                    },
                    ["immersive"] = new Dictionary<int, string>
                    {
                        { 2, "50706102"},
                        { 4, "50706104"},
                        { 8, "50706108"},
                        { 10, "-1"},
                    }
                },

                ["Pam"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 9, "503180"}, // (BUILD THE HOUSE DAMN)
                        { 10, "-1"},
                    }
                },

                ["Pierre"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "16"}, // (not really relevent? about secret stash)
                        { 10, "-1"},
                    }
                },

                ["Robin"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "33"},
                        { 10, "-1"},
                    }
                },

                ["Evelyn"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "19"},
                        { 10, "-1"},
                    }
                }
            };
        }

        private Dictionary<string, Dictionary<string, Dictionary<int, string>>> InitializeEventsData_SVE()
        {
            return new()
            {
                ["Shane"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "611944"},
                        { 4, "3910674"},
                        { 6, "2118991"},
                        { 7, "3910974" },
                        { 8, "3900074"},
                        { 10, "9581348"},
                        { 12, "1836459" },
                        { 14, "-1"}
                    },
                    ["immersive"] = new Dictionary<int, string>
                    {
                        { 2, "611944"},
                        { 4, "3910674"},
                        { 6, "2118991"},
                        { 7, "3910974" },
                        { 8, "9581348"},
                        { 10, "59443111"},
                        { 12, "1836459" },
                        { 14, "-1"}
                    }
                },

                ["Alex"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "2481135"},
                        { 5, "21"},
                        { 6, "2119820"},
                        { 8, "288847"},
                        { 10, "911526"},
                        { 14, "-1"}
                    }
                },

                ["Sebastian"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "2794460"},
                        { 4, "384883"},
                        { 6, "27"},
                        { 8, "29"},
                        { 10, "384882"},
                        { 14, "-1"}
                    }
                },

                ["Sam"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "44"},
                        { 4, "46"},
                        { 5, "1000004" },
                        { 6, "45"},
                        { 8, "4081148"},
                        { 10, "233104"},
                        { 14, "-1"}
                    }
                },

                ["Harvey"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "56"},
                        { 4, "57"},
                        { 5, "1000003" },
                        { 6, "58"},
                        { 8, "571102"},
                        { 10, "528052"},
                        { 14, "-1"}
                    }
                },

                ["Elliott"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "39"},
                        { 4, "40"},
                        { 6, "423502"},
                        { 8, "1848481"},
                        { 10, "43"},
                        { 14, "-1"}
                    }
                },

                ["Abigail"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "1"},
                        { 4, "2"},
                        { 5, "1140961" },
                        { 6, "4"},
                        { 8, "3"},
                        { 10, "901756"},
                        { 12, "1140962"},
                        { 14, "-1"}
                    }
                },

                ["Leah"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "50"},
                        { 4, "51"},
                        { 6, "52"},
                        { 8, "55"},
                        { 10, "54"},
                        { 14, "-1"}
                    }
                },

                ["Maru"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "6"},
                        { 4, "7"},
                        { 5, "1000011" },
                        { 6, "8"},
                        { 8, "9"},
                        { 10, "10"},
                        { 14, "-1"}
                    }
                },

                ["Penny"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "34"}, // (geroge wheelchair)
                        { 4, "35"},
                        { 6, "36"},
                        { 8, "181928"},
                        { 10, "38"},
                        { 14, "-1"}
                    }
                },

                ["Haley"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "11"},
                        { 4, "12"},
                        { 6, "13"},
                        { 8, "14"},
                        { 10, "15"},
                        { 14, "-1"}
                    }
                },

                ["Emily"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "471942"},
                        { 4, "463391"},
                        { 5, "1000008" },
                        { 6, "917409"},
                        { 8, "2123243"},
                        { 10, "2123343"},
                        { 14, "-1"}
                    }
                },

                ["Willy"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "711130"},
                        { 10, "-1"},
                    }
                },

                ["Vincent"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 10, "-1"},
                    }
                },

                ["Wizard"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 10, "-1"},
                    }
                },

                ["Gus"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "96"},
                        { 5, "980558"},
                        { 10, "-1"},
                    }
                },

                ["Demetrius"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "25"},
                        { 10, "-1"},
                    }
                },

                ["Jas"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 1, "7142999" },
                        { 8, "1000021" }, //MATURE
                        { 10, "-1"},
                    },
                    ["immersive"] = new Dictionary<int, string>
                    {
                        { 1, "7142999" },
                        { 2, "50706112"},
                        { 4, "50706113"},
                        { 7, "50706114"},
                        //{ 8, "50706115"},
                        { 8, "1000021" }, //MATURE
                        { 10, "-1"},
                    }
                },

                ["Jodi"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "94"},
                        { 10, "-1"},
                    }
                },

                ["George"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "18"},
                        { 10, "-1"},
                    }
                },

                ["Kent"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "100"},
                        { 10, "-1"},
                    }
                },

                ["Clint"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 3, "97"},
                        { 6, "101"},
                        { 8, "1000014" },
                        { 10, "-1"},
                    },
                    ["no_emily"] = new Dictionary<int, string>
                    {
                        { 3, "97"},
                        { 8, "1000014" },
                        { 10, "-1"},
                    }
                },

                ["Krobus"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "1090503" }, //NO JOJA
                        { 6, "1090504" },
                        { 10, "-1"},
                    }
                },

                ["Caroline"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "719926"},
                        { 6, "17"},
                        { 10, "-1"},
                    }
                },

                ["Leo"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "6497423"},
                        { 4, "6497421"},
                        { 6, "6497428"},
                        { 9, "8959199"},
                        { 10, "-1"},
                    }
                },

                ["Linus"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "26"},
                        { 8, "371652"},
                        { 10, "-1"},
                    }
                },

                ["Lewis"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "639373"},
                        { 10, "-1"},
                    }
                },

                ["Marnie"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 3, "92"},
                        { 6, "639373"},
                        { 8, "1000026" },
                        { 10, "-1"},
                    },
                    ["immersive"] = new Dictionary<int, string>
                    {
                        { 2, "50706102"},
                        { 4, "50706104"},
                        { 8, "50706108"},
                        //{ 8, "1000026" },
                        { 10, "-1"},
                    }
                },

                ["Pam"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 9, "503180"}, // (BUILD THE HOUSE DAMN)
                        { 10, "-1"},
                    }
                },

                ["Pierre"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "16"}, // (not really relevent? about secret stash)
                        { 10, "-1"},
                    }
                },

                ["Robin"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 6, "33"},
                        { 10, "-1"},
                    }
                },

                ["Evelyn"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "19"},
                        { 10, "-1"},
                    }
                },

                ["Sandy"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 5, "1000030"},
                        //{ 5, "1000038"},
                    }
                },

                ["Claire"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "3219871"},
                        { 4, "3219872"},
                        { 6, "3219873" },
                        { 8, "3219874" },
                        { 10, "3219876" },
                        { 12, "3219880" },
                        { 14, "-1" }
                    }
                },

                ["Lance"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "6951320"},
                        { 6, "65360190" },
                        { 8, "65360191" },
                        { 10, "65360194" },
                        { 12, "65360198" },
                        { 14, "-1" }
                    }
                },

                ["Wizard"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "1000075"},
                        { 4, "1724095"},
                        { 6, "1724096" },
                        { 8, "1724097" },
                        { 10, "1724098" },
                        { 14, "-1" }
                    }
                },

                ["Olivia"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "4000001"},
                        { 4, "4000002"},
                        { 6, "4000004" },
                        { 8, "4000003" },
                        { 10, "8982806" },
                        { 12, "8982808" },
                        { 14, "-1" }
                    }
                },

                ["Sophia"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "8185291"},
                        { 4, "8185292"},
                        { 6, "8185294" },
                        { 7, "8185298" },
                        { 8, "8185295" },
                        { 10, "8185296" },
                        { 12, "8185300" },
                        { 14, "-1" }
                    }
                },

                ["Victor"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "2000000"},
                        { 3, "2000050" }, //no 10 hearts scene
                        { 4, "2000001"},
                        { 6, "5553210" },
                        { 8, "2000003" },
                        { 10, "2000004" },
                        { 12, "2000006" },
                        { 14, "-1" }
                    }
                },

                ["Andy"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "5000001"},
                        { 4, "5000009"},
                        { 6, "5000002" },
                        { 8, "5000003" },
                        { 10, "-1" }
                    }
                },

                ["Apples"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "7776002"},
                        { 4, "7776003"},
                        { 6, "7776005" },
                        { 8, "7776007" },
                        { 10, "-1" }
                    }
                },

                ["Gunther"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "2468000"},
                        { 4, "2468001"},
                        { 6, "2468004" },
                        { 10, "-1" }
                    }
                },

                ["MarlonFay"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 4, "1004200"},
                        { 10, "-1" }
                    }
                },

                ["Martin"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "3219970"},
                        { 4, "3219971"},
                        { 6, "3219972" },
                        { 10, "-1" }
                    }
                },

                ["Morgan"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "5978929"},
                        { 4, "5978935"},
                        { 6, "5978936" },
                        { 8, "5978937" },
                        { 10, "-1" }
                    }
                },

                ["MorrisTod"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "6663501"},
                        { 4, "6663411"},
                        { 6, "6663915" },
                        { 8, "6665290" },
                        { 10, "-1" }
                    }
                },

                ["Susan"] = new()
                {
                    ["vanilla"] = new Dictionary<int, string>
                    {
                        { 2, "1000078"},
                        { 4, "1000079"},
                        { 7, "1000080" },
                        { 10, "-1" }
                    }
                }
            };
        }
    }
}
