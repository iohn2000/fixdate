﻿using fixDate;
using fixDate.interfaces;
using Microsoft.Extensions.DependencyInjection;


/*
 BUG : 
Changed: .\2022\lzk-2022-11-28\WhatsApp Image 2022-11-25 at 16.38.30.jpeg date changed to:2022-11-25 00:00:00Z.
Changed: .\2022\lzk-2022-11-28\WhatsApp Image 2022-11-25 at 16.38.41.jpeg date changed to:2022-11-25 00:00:00Z.
Changed: .\2022\lzk-2022-11-28\WhatsApp Image 2022-11-25 at 16.38.48.jpeg date changed to:2022-11-25 00:00:00Z.
 */

namespace haha // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f">force update file date even if it is already correct</param>
        /// <param name="p">relative path where to find documents to check, default is current path</param>
        static void Main(bool f, string p = @".\")
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IFixDates, FixDates>();
            services.AddSingleton<IFileNameReader, FileNameReader>();
            services.AddSingleton<IConfigurationReader, ConfigurationReaderHardCoded>();
            services.AddSingleton<IDateMatch, DateMatcher>();
            services.AddSingleton<IDateParsing, DateParsing>();
            try
            {
                Executor? executerService =services.AddSingleton<Executor, Executor>()
                        .BuildServiceProvider()
                        .GetService<Executor>();
                if (executerService != null) 
                    executerService.Execute(f, p);
                else
                {
                    Console.WriteLine($"Error getting executor service, response is null");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}