using JoberMQ.Library.StatusCode.Enums;
using JoberMQ.Library.StatusCode.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
     0             .0            .0
 Application / Project Layer / Number
 
Application
1 - Server (JoberMQ)
2 - Client (JoberMQ.Client.Net)

Project Layer
1 - JoberMQ.Configuration
2 - JoberMQ.State
3 - JoberMQ.Database
4 - JoberMQ.Client
5 - JoberMQ.Queue
6 - JoberMQ.Distributor
7 - JoberMQ.Broker
8 - JoberMQ.Timing
9 - JoberMQ.Publisher
 */

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultStatusCodeConst
    {
        internal const StatusCodeFactoryEnum StatusCodeFactory = StatusCodeFactoryEnum.Default;
        internal const StatusCodeMessageLanguageEnum StatusCodeMessageLanguage = StatusCodeMessageLanguageEnum.tr;
        internal static ConcurrentDictionary<string, StatusCodeModel> DefaultStatusCodeDatas = DefaultStatusCodeData();
        private static ConcurrentDictionary<string, StatusCodeModel> DefaultStatusCodeData()
        {
            var datas = new ConcurrentDictionary<string, StatusCodeModel>();


            #region JoberMQ.Distributor
            datas.TryAdd("1.6.1", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.6.1",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "isminde bir queue yok."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            #endregion

            #region JoberMQ.Broker
            datas.TryAdd("1.7.1", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.1",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veritabanından distributor ler alındı."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.2", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.2",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veritabanından distributor ler alınırken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.3", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.3",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veritabanından queue ler alındı."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.4", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.4",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veritabanından queue ler alınırken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.5", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.5",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Default distributor ler oluşturuldu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.6", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.6",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Default distributor ler oluşturulurken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.7", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.7",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Default queue ler oluşturuldu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.8", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.8",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Default queue ler oluşturulurken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.9", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.9",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Mesajlar Queue içerisine aktarıldı."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.10", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.10",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Mesajlar Queue içerisine aktarılırken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });


            datas.TryAdd("1.7.51", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.51",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "isminde bir distributor yok."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });

            #endregion

            #region JoberMQ.Timing
            datas.TryAdd("1.8.51", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.8.51",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "JobTransaction eklenirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.8.52", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.8.52",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Job eklenirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            #endregion





            #region MyRegion
            datas.TryAdd("0.0.1", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.1",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Sunucu başlatılamadı, bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "The server could not be started, an error occurred."
                    }
                }
            });

            datas.TryAdd("0.0.2", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.2",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veritabanı oluşturulurken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while creating the database."
                    }
                }
            });

            datas.TryAdd("0.0.3", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.3",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veriler içe aktarılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while importing data."
                    }
                }
            });

            datas.TryAdd("0.0.4", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.4",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Zamanlanmış görevler oluşturulurken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while creating scheduled tasks."
                    }
                }
            });

            datas.TryAdd("0.0.5", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.5",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veri kontrolü yapılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while checking the data."
                    }
                }
            });

            datas.TryAdd("0.0.6", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.6",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Veri kontrol zamanlayıcısı başlatılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while starting the data check timer."
                    }
                }
            });

            datas.TryAdd("0.0.7", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.7",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Mesajlar içe aktarılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while importing messages."
                    }
                }
            });

            datas.TryAdd("0.0.8", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.8",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Hata mesajları içe aktarılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while importing error messages."
                    }
                }
            });

            datas.TryAdd("0.0.9", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.9",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Tamamlanan görevleri silecek zamanlayıcı başlatılırken bir hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while starting the scheduler that will delete completed tasks."
                    }
                }
            });

            datas.TryAdd("0.0.10", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.10",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Aynı ClientKey'e sahip, birden fazla oturum açamaz."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "Cannot login more than one with the same ClientKey."
                    }
                }
            });

            datas.TryAdd("0.0.11", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.11",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Kullanıcı bilgileriniz yanlış."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "Your user information is incorrect."
                    }
                }
            });

            datas.TryAdd("0.0.12", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.12",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Sunucuya erişilemedi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "The server could not be reached."
                    }
                }
            });

            datas.TryAdd("0.0.13", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.13",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Sunucu hazırlanıyor, erişemezsiniz."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "The server is being prepared, you cannot access it."
                    }
                }
            });

            datas.TryAdd("0.0.14", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "0.0.14",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Client eklenirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = "An error occurred while adding the client."
                    }
                }
            }); 
            #endregion

            datas.TryAdd("1.7.1", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.1",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Bu isimde mevcut bir distributor var."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.2", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.2",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Distributor eklendi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.3", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.3",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Distributor eklenirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });

            datas.TryAdd("1.7.11", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.11",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Bu isimde bir distributor yok."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.12", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.12",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Default Distributörler üzerinde değişiklik yapılamaz."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.13", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.13",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Distributor güncellendi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.14", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.14",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Distributor güncellenirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });

            datas.TryAdd("1.7.15", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.15",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Default Distributörler silinemez."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.16", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.16",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Distributor'e bağlı kuyruk var, silemezsiniz."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.17", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.17",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Distributor silindi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.18", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.18",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Distributor silinirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });

            datas.TryAdd("1.7.51", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.51",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Bu isimde mevcut bir queue var."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.52", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.52",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Queue eklendi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.53", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.53",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Queue eklenirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            
            datas.TryAdd("1.7.54", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.54",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Bu isimde bir queue yok."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.55", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.55",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Default Queue ler üzerinde değişiklik yapılamaz."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.56", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.56",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Queue güncellendi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.57", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.57",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Queue güncellenirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });

            datas.TryAdd("1.7.58", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.58",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Default Queue ler silinemez."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.59", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.59",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "İçerisinde mesaj olan Queue ler silinemez."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.60", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.60",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Queue silindi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.61", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.61",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Queue silinirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });

            datas.TryAdd("1.7.62", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Information,
                StatusCode = "1.7.62",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Bind işlemi gerçekleştirildi."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });
            datas.TryAdd("1.7.63", new StatusCodeModel
            {
                StatusCodeType = StatusCodeTypeEnum.Error,
                StatusCode = "1.7.63",
                StatusCodeMessages = new List<StatusCodeMessageModel>
                {
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.tr,
                        Message = "Bind işlemi gerçekleştirilirken hata oluştu."
                    },
                    new StatusCodeMessageModel
                    {
                        Language = StatusCodeMessageLanguageEnum.en,
                        Message = ""
                    }
                }
            });


            return datas;
        }
    }
}
