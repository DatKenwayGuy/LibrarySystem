﻿namespace LibraryStandard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Library;

    using LibraryStandard.Helpers;

    public class Program
    {
        public static void Main(string[] args)
        {

            PublicLibrary.Init();
            StandardMessages.WelcomeImage();

            var menu = new EasyConsole.Menu()
                .Add("Catalog", () =>
                {
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("View all the books", () =>
                {
                    Catalog.Instance.GetBookList();
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Search a book", () => //supermenu
                {
                    StandardMessages.PressKeyToContinue(); 
                })
                .Add("Search a book through ID", () => //submenu
                {
                    StandardMessages.WriteInputBelow();
                    Catalog.Instance.SearchBookByID(Console.ReadLine());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Search a book through ISBN", () => //submenu
                {
                    StandardMessages.WriteInputBelow();
                    Catalog.Instance.SearchBookByIsbn(Console.ReadLine());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Search a book by the name of the author", () => //submenu
                {
                    StandardMessages.WriteInputBelow();
                    var searchRes = Catalog.Instance.SearchBookByAuthor(Console.ReadLine());
                    Console.WriteLine(searchRes.First().ISBN);
                    StandardMessages.ResultsCount(searchRes.Count());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Search a book by title", () => //submenu
                {
                    StandardMessages.WriteInputBelow();
                    Catalog.Instance.SearchBookByTitle(Console.ReadLine());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Library information", () =>
                {
                    Console.WriteLine($"This is the very elaborate information page of the library system.");
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Login", () =>
                {
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Help", () =>
                {
                    Console.WriteLine($"This is the very elaborate help page of the library system.");
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })

            //these are accessible only when logged in
                .Add("Delete a single book from the catalog", () =>
                {
                    Console.WriteLine($"Please input the ID of the book that you want to delete from the catalog.");
                    string input = Console.ReadLine();
                    if (StandardMessages.AreYouSure().Equals(true))
                    {
                        Catalog.Instance.RemoveBook(input);
                    }

                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();

                })
                .Add("Delete all books from the catalog", () =>
                {
                    if (StandardMessages.AreYouSure().Equals(true))
                    {
                        Catalog.Instance.DeleteAllBooks();
                    }

                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Add a new book to the catalog", () => //needs attention!!
                {
                    //Catalog.Instance.AddNewBook();
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Add an existing book to the catalog", () => 
                {
                    
                    Catalog.Instance.AddExistingBook(Console.ReadLine());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Create a backup of the books", () =>
                {
                    Console.WriteLine(@"Choose a filepath, for an instance ''.\Backups\Bookbackup2.json''");
                    BackUp.Create(Console.ReadLine());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Load a backup of the books to the current program.", () =>
                {
                    Console.WriteLine(@"Choose a filepath, for an instance ''.\Backups\Bookbackup2.json''");
                    BackUp.RestoreFromBackup<List<Book>>(Console.ReadLine());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                })
                .Add("Exit", () =>
                    {
                        Environment.Exit(0);
                    });


            //.Add("Exit", () =>
            // {
            //     Environment.Exit(0);
            // })

            while (true)
            {
                menu.Display();
            }
            
           
            // StandardMessages.WelcomeImage();
            // StandardMessages.PressAnyKey();
            




            string StrBooks = DataOperator.ReadFromFile(Constants.BooksDataFile);
            Console.WriteLine(StrBooks);


            List<Book> BookList = new List<Book>();
            BookList = DataOperator.DeserializeJson<List<Book>>(StrBooks);
            BookList.First().ShowBookProp();
            Catalog.Instance.SetBookList(BookList);

            //DataOperator.WriteToFile(StrBooks, Constants.bookbackup2);
            Console.WriteLine($"making a book by rocky");
            Book testbook = new BookBuilder()
                .WithAuthorName("Rocky Dexter")
                .WithTitle("Love for Jordan")
                .WithYear(2009)
                .WithPages(5)
                .WithLanguage("bird")
                .WithImageLink("linklink")
                .WithLink("link")
                .WithISBN("34023529852")
                .WithCountry("Netherlands")
                .CreateBook();

            Console.WriteLine($"adding the rocky book to the list of books");
            Catalog.Instance.AddNewBook(testbook);
            Console.WriteLine($"showing some props of the book as first book from the list");
            Catalog.Instance.GetBookList().Last().ShowBookProp();
            Console.WriteLine($"Now making a backup instance");
            BackUp firstbackup = new BackUp();
            Console.WriteLine($"now writing the serialised booklist to a file (making a backup)");
            BackUp.Create(Constants.bookbackup2);
            Console.WriteLine($"Showing props from restored backup list");
            BackUp.RestoreFromBackup<List<Book>>(Constants.bookbackup2).First().ShowBookProp();









            //List<Book> newbooklist = new List<Book>();
            //newbooklist = DataOperator.Load<Book>("Data\\Books.json");

            //BackUp firstbackup = new BackUp();
            //firstbackup.Create(BackUp.backupnum.Backupnumber2);
            /*List<Book> testlist= new List<Book>();
            
            testlist.Add(testbook);
            firstbackup.TempCreateWritebackup(testlist);*/

            Console.WriteLine("I guess it worked");
            StandardMessages.PressAnyKey();
            Console.ReadLine();


            /*Console.WriteLine("Welcome User");
            //catalog.LoadBookFile(@"Data\Books.json");
            Book abc = new Book();
            foreach (Book bk in Catalog.Instance.SearchBookByAuthor("Andersen"))
            {
                Console.WriteLine("Found a book :)");
                bk.ShowBookProp();
                abc = bk;
            }

            Console.Write("before...");
            Console.ReadLine();
            
            catalog.RemoveBook(catalog.SearchBookByID(abc.ID).ID);

            Console.Write("after...");
            Console.ReadLine();*/
            
            /*
            foreach (Book bk in catalog.SearchBookByAuthor("Andersen"))
            {
                Console.WriteLine("Found a book :)");
                bk.ShowBookProp();
            }
            Console.Write(">");
            Console.ReadLine();

            BookBuilder Noordhoff = new BookBuilder();
            Book JRaf_see_stuff = Noordhoff
                                .WithAuthorName("JRAF")
                                //.WithAvailability(true)
                                .WithCountry("Netherlands")
                                .WithLanguage("English")
                                .WithPages(250)
                                .WithYear(2018)
                                .WithTitle("A whole life")
                                .WithImageLink("home")
                                .CreateBook();

            Book new_book_one = new BookBuilder()
                .WithAuthorName("Vincent Van Andersen")
                .WithCountry("Netherlands")
                .WithLanguage("English")
                .WithPages(250)
                .WithYear(2018)
                .WithTitle("A whole life of me")
                .WithImageLink("home")
                .CreateBook();

            Book new_book_two = new BookBuilder()
                .WithAuthorName("Vincent v. Andersen")
                .WithCountry("Netherlands")
                .WithLanguage("English")
                .WithPages(250)
                .WithYear(2018)
                .WithTitle("A whole life of me")
                .WithImageLink("home")
                .CreateBook();

            JRaf_see_stuff.ShowBookProp();

            Console.Write(">");
            Console.ReadLine();

            catalog.AddNewBook(JRaf_see_stuff);

            Console.WriteLine("Looking for freshly added book :)");
            foreach (Book book in catalog.SearchBookByAuthor("JRaf"))
            {
                Console.WriteLine("Found a book :)");
                book.ShowBookProp();
            }

    */
            Console.Write(">");
            Console.ReadLine();




        }
    }
}
