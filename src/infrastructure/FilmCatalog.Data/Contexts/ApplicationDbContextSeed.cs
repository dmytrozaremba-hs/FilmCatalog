using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using FilmCatalog.Domain.Enums;
using FilmCatalog.Domain.Events;
using Microsoft.Extensions.Logging;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace FilmCatalog.Data.Contexts;

public static class ApplicationDbContextSeed
{

    public static async Task SeedInitialDataAsync(ApplicationDbContext context, IIdentityService identityService)
    {
        if (context.Users.Any())
        {
            // already seeded
            return;
        }


        var adminUser1 = await identityService.CreateUserAsync("admin1@test.com", "pass", "admin1", Role.Administrator);
        var adminUser2 = await identityService.CreateUserAsync("admin2@test.com", "pass", "admin2", Role.Administrator);

        var regularUser1 = await identityService.CreateUserAsync("user1@test.com", "pass", "user1", Role.Regular);
        var regularUser2 = await identityService.CreateUserAsync("user2@test.com", "pass", "user2", Role.Regular);
        var dmytro = await identityService.CreateUserAsync("dmytro@test.com", "pass", "dmytro", Role.Regular);
        var tom = await identityService.CreateUserAsync("tom@test.com", "pass", "tom", Role.Regular);
        var daniel = await identityService.CreateUserAsync("daniel@test.com", "pass", "daniel", Role.Regular);
        var mike = await identityService.CreateUserAsync("mike@test.com", "pass", "mike", Role.Regular);
        var john = await identityService.CreateUserAsync("john@test.com", "pass", "john", Role.Regular);
        var angry_reviewer = await identityService.CreateUserAsync("angry_reviewer@test.com", "pass", "angry_reviewer", Role.Regular);
        var sherlock = await identityService.CreateUserAsync("sherlock@test.com", "pass", "sherlock", Role.Regular);
        var kate = await identityService.CreateUserAsync("kate@test.com", "pass", "kate", Role.Regular);
        var sussy = await identityService.CreateUserAsync("sussy@test.com", "pass", "sussy", Role.Regular);
        var missy = await identityService.CreateUserAsync("missy@test.com", "pass", "missy", Role.Regular);
        var george = await identityService.CreateUserAsync("george@test.com", "pass", "george", Role.Regular);

        var users = new List<User>
        {
            dmytro.AuthenticationResult.User,
            tom.AuthenticationResult.User,
            daniel.AuthenticationResult.User,
            mike.AuthenticationResult.User,
            john.AuthenticationResult.User,
            sherlock.AuthenticationResult.User,
            kate.AuthenticationResult.User,
            sussy.AuthenticationResult.User,
            missy.AuthenticationResult.User,
            george.AuthenticationResult.User,
            angry_reviewer.AuthenticationResult.User
        };

        // Create genre entities
        var actionGenre = new Genre { Name = "Action" };
        var adventureGenre = new Genre { Name = "Adventure" };
        var animatedGenre = new Genre { Name = "Animated" };
        var biographyGenre = new Genre { Name = "Biography" };
        var comedyGenre = new Genre { Name = "Comedy" };
        var crimeGenre = new Genre { Name = "Crime" };
        var danceGenre = new Genre { Name = "Dance" };
        var disasterGenre = new Genre { Name = "Disaster" };
        var documentaryGenre = new Genre { Name = "Documentary" };
        var dramaGenre = new Genre { Name = "Drama" };
        var eroticGenre = new Genre { Name = "Erotic" };
        var familyGenre = new Genre { Name = "Family" };
        var fantasyGenre = new Genre { Name = "Fantasy" };
        var foundFootageGenre = new Genre { Name = "Found Footage" };
        var historicalGenre = new Genre { Name = "Historical" };
        var horrorGenre = new Genre { Name = "Horror" };
        var independentGenre = new Genre { Name = "Independent" };
        var legalGenre = new Genre { Name = "Legal" };
        var liveActionGenre = new Genre { Name = "Live Action" };
        var martialArtsGenre = new Genre { Name = "Martial Arts" };
        var musicalGenre = new Genre { Name = "Musical" };
        var mysteryGenre = new Genre { Name = "Mystery" };
        var noirGenre = new Genre { Name = "Noir" };
        var performanceGenre = new Genre { Name = "Performance" };
        var politicalGenre = new Genre { Name = "Political" };
        var romanceGenre = new Genre { Name = "Romance" };
        var satireGenre = new Genre { Name = "Satire" };
        var scienceFictionGenre = new Genre { Name = "Science Fiction" };
        var shortGenre = new Genre { Name = "Short" };
        var silentGenre = new Genre { Name = "Silent" };
        var slasherGenre = new Genre { Name = "Slasher" };
        var sportsGenre = new Genre { Name = "Sports" };
        var spyGenre = new Genre { Name = "Spy" };
        var superheroGenre = new Genre { Name = "Superhero" };
        var supernaturalGenre = new Genre { Name = "Supernatural" };
        var suspenseGenre = new Genre { Name = "Suspense" };
        var teenGenre = new Genre { Name = "Teen" };
        var thrillerGenre = new Genre { Name = "Thriller" };
        var warGenre = new Genre { Name = "War" };
        var westernGenre = new Genre { Name = "Western" };
        var animationGenre = new Genre { Name = "Animation" };

        // Adding genres to the context
        context.Genres.AddRange(
            actionGenre,
            adventureGenre,
            animatedGenre,
            biographyGenre,
            comedyGenre,
            crimeGenre,
            danceGenre,
            disasterGenre,
            documentaryGenre,
            dramaGenre,
            eroticGenre,
            familyGenre,
            fantasyGenre,
            foundFootageGenre,
            historicalGenre,
            horrorGenre,
            independentGenre,
            legalGenre,
            liveActionGenre,
            martialArtsGenre,
            musicalGenre,
            mysteryGenre,
            noirGenre,
            performanceGenre,
            politicalGenre,
            romanceGenre,
            satireGenre,
            scienceFictionGenre,
            shortGenre,
            silentGenre,
            slasherGenre,
            sportsGenre,
            spyGenre,
            superheroGenre,
            supernaturalGenre,
            suspenseGenre,
            teenGenre,
            thrillerGenre,
            warGenre,
            westernGenre,
            animationGenre
        );

        // Create tag entities
        var actionTag = new Tag { Name = "Action" };
        var adventureTag = new Tag { Name = "Adventure" };
        var comedyTag = new Tag { Name = "Comedy" };
        var dramaTag = new Tag { Name = "Drama" };
        var romanceTag = new Tag { Name = "Romance" };
        var thrillerTag = new Tag { Name = "Thriller" };
        var scienceFictionTag = new Tag { Name = "Science Fiction" };
        var horrorTag = new Tag { Name = "Horror" };
        var fantasyTag = new Tag { Name = "Fantasy" };
        var mysteryTag = new Tag { Name = "Mystery" };
        var historicalDramaTag = new Tag { Name = "Historical Drama" };
        var animationTag = new Tag { Name = "Animation" };
        var musicalTag = new Tag { Name = "Musical" };
        var crimeThrillerTag = new Tag { Name = "Crime Thriller" };
        var sportsDramaTag = new Tag { Name = "Sports Drama" };
        var warEpicTag = new Tag { Name = "War Epic" };
        var psychologicalHorrorTag = new Tag { Name = "Psychological Horror" };
        var sciFiActionTag = new Tag { Name = "Sci-Fi Action" };
        var popularTag = new Tag { Name = "Popular" };
        var classicTag = new Tag { Name = "Classic" };
        var superheroTag = new Tag { Name = "Superhero" };
        var mindBendingTag = new Tag { Name = "Mind-Bending" };
        var cultClassicTag = new Tag { Name = "Cult Classic" };
        var spaceTravelTag = new Tag { Name = "Space Travel" };
        var heartwarmingTag = new Tag { Name = "Heartwarming" };
        var psychologicalThrillerTag = new Tag { Name = "Psychological Thriller" };
        var alternateHistoryTag = new Tag { Name = "Alternate History" };
        var familyFriendlyTag = new Tag { Name = "Family-Friendly" };
        var suspensefulTag = new Tag { Name = "Suspenseful" };
        var trueStoryTag = new Tag { Name = "True Story" };
        var thoughtProvokingTag = new Tag { Name = "Thought-Provoking" };
        var musicTag = new Tag { Name = "Music" };
        var romanticTag = new Tag { Name = "Romantic" };

        // Adding tags to the context
        context.Tags.AddRange(
            actionTag,
            adventureTag,
            comedyTag,
            dramaTag,
            romanceTag,
            thrillerTag,
            scienceFictionTag,
            horrorTag,
            fantasyTag,
            mysteryTag,
            historicalDramaTag,
            animationTag,
            musicalTag,
            crimeThrillerTag,
            familyFriendlyTag,
            superheroTag,
            sportsDramaTag,
            warEpicTag,
            psychologicalHorrorTag,
            sciFiActionTag,
            classicTag,
            superheroTag,
            mindBendingTag,
            cultClassicTag,
            spaceTravelTag,
            heartwarmingTag,
            psychologicalThrillerTag,
            alternateHistoryTag,
            familyFriendlyTag,
            suspensefulTag,
            trueStoryTag,
            thoughtProvokingTag,
            musicTag,
            romanticTag
        );

        context.SaveChanges();


        // Assuming you have a context variable representing your database context

        // Create actor entities
        var downey = new Person
        {
            FirstName = "Robert",
            LastName = "Downey Jr.",
            IsActor = true,
            BirthDate = new DateTime(1965, 4, 4),
            ActedInFilms = new List<Film>()
        };

        var hanks = new Person
        {
            FirstName = "Tom",
            LastName = "Hanks",
            IsActor = true,
            BirthDate = new DateTime(1956, 7, 9),
            ActedInFilms = new List<Film>()
        };

        // Add more actors here

        var pitt = new Person
        {
            FirstName = "Brad",
            LastName = "Pitt",
            IsActor = true,
            BirthDate = new DateTime(1963, 12, 18),
            ActedInFilms = new List<Film>()
        };

        var dicaprio = new Person
        {
            FirstName = "Leonardo",
            LastName = "DiCaprio",
            IsActor = true,
            BirthDate = new DateTime(1974, 11, 11),
            ActedInFilms = new List<Film>()
        };

        var depp = new Person
        {
            FirstName = "Johnny",
            LastName = "Depp",
            IsActor = true,
            BirthDate = new DateTime(1963, 6, 9),
            ActedInFilms = new List<Film>()
        };

        var streep = new Person
        {
            FirstName = "Meryl",
            LastName = "Streep",
            IsActor = true,
            BirthDate = new DateTime(1949, 6, 22),
            ActedInFilms = new List<Film>()
        };

        var jackman = new Person
        {
            FirstName = "Hugh",
            LastName = "Jackman",
            IsActor = true,
            BirthDate = new DateTime(1968, 10, 12),
            ActedInFilms = new List<Film>()
        };

        var stone = new Person
        {
            FirstName = "Emma",
            LastName = "Stone",
            IsActor = true,
            BirthDate = new DateTime(1988, 11, 6),
            ActedInFilms = new List<Film>()
        };

        var reynolds = new Person
        {
            FirstName = "Ryan",
            LastName = "Reynolds",
            IsActor = true,
            BirthDate = new DateTime(1976, 10, 23),
            ActedInFilms = new List<Film>()
        };

        var portman = new Person
        {
            FirstName = "Natalie",
            LastName = "Portman",
            IsActor = true,
            BirthDate = new DateTime(1981, 6, 9),
            ActedInFilms = new List<Film>()
        };

        var lawrence = new Person
        {
            FirstName = "Jennifer",
            LastName = "Lawrence",
            IsActor = true,
            BirthDate = new DateTime(1990, 8, 15),
            ActedInFilms = new List<Film>()
        };

        var evans = new Person
        {
            FirstName = "Chris",
            LastName = "Evans",
            IsActor = true,
            BirthDate = new DateTime(1981, 6, 13),
            ActedInFilms = new List<Film>()
        };

        var johansson = new Person
        {
            FirstName = "Scarlett",
            LastName = "Johansson",
            IsActor = true,
            BirthDate = new DateTime(1984, 11, 22),
            ActedInFilms = new List<Film>()
        };

        var smith = new Person
        {
            FirstName = "Will",
            LastName = "Smith",
            IsActor = true,
            BirthDate = new DateTime(1968, 9, 25),
            ActedInFilms = new List<Film>()
        };

        var johnson = new Person
        {
            FirstName = "Dwayne",
            LastName = "Johnson",
            IsActor = true,
            BirthDate = new DateTime(1972, 5, 2),
            ActedInFilms = new List<Film>()
        };

        var hathaway = new Person
        {
            FirstName = "Anne",
            LastName = "Hathaway",
            IsActor = true,
            BirthDate = new DateTime(1982, 11, 12),
            ActedInFilms = new List<Film>()
        };

        // Add the actors to the context
        context.Persons.AddRange(downey, hanks, pitt, dicaprio, depp, streep, jackman, stone, reynolds, portman,
                                 lawrence, evans, johansson, smith, johnson, hathaway);


        // Create director entities
        var nolan = new Person
        {
            FirstName = "Christopher",
            LastName = "Nolan",
            IsDirector = true,
            BirthDate = new DateTime(1970, 7, 30),
            DirectedFilms = new List<Film>()
        };

        var spielberg = new Person
        {
            FirstName = "Steven",
            LastName = "Spielberg",
            IsDirector = true,
            BirthDate = new DateTime(1946, 12, 18),
            DirectedFilms = new List<Film>()
        };

        // Add more directors here

        var scorsese = new Person
        {
            FirstName = "Martin",
            LastName = "Scorsese",
            IsDirector = true,
            BirthDate = new DateTime(1942, 11, 17),
            DirectedFilms = new List<Film>()
        };

        var tarantino = new Person
        {
            FirstName = "Quentin",
            LastName = "Tarantino",
            IsDirector = true,
            BirthDate = new DateTime(1963, 3, 27),
            DirectedFilms = new List<Film>()
        };

        var villeneuve = new Person
        {
            FirstName = "Denis",
            LastName = "Villeneuve",
            IsDirector = true,
            BirthDate = new DateTime(1967, 10, 3),
            DirectedFilms = new List<Film>()
        };

        var gerwig = new Person
        {
            FirstName = "Greta",
            LastName = "Gerwig",
            IsDirector = true,
            BirthDate = new DateTime(1983, 8, 4),
            DirectedFilms = new List<Film>()
        };

        var bong = new Person
        {
            FirstName = "Bong",
            LastName = "Joon Ho",
            IsDirector = true,
            BirthDate = new DateTime(1969, 9, 14),
            DirectedFilms = new List<Film>()
        };

        var lee = new Person
        {
            FirstName = "Spike",
            LastName = "Lee",
            IsDirector = true,
            BirthDate = new DateTime(1957, 3, 20),
            DirectedFilms = new List<Film>()
        };

        var jenkins = new Person
        {
            FirstName = "Barry",
            LastName = "Jenkins",
            IsDirector = true,
            BirthDate = new DateTime(1979, 11, 19),
            DirectedFilms = new List<Film>()
        };

        var peele = new Person
        {
            FirstName = "Jordan",
            LastName = "Peele",
            IsDirector = true,
            BirthDate = new DateTime(1979, 2, 21),
            DirectedFilms = new List<Film>()
        };

        // Add the directors to the context
        context.Persons.AddRange(nolan, spielberg, scorsese, tarantino, villeneuve, gerwig, bong, lee, jenkins, peele);


        // Creating named entities for producers
        var feige = new Person
        {
            FirstName = "Kevin",
            LastName = "Feige",
            IsProducer = true,
            BirthDate = new DateTime(1973, 6, 2),
            ProducedFilms = new List<Film>()
        };

        var kennedy = new Person
        {
            FirstName = "Kathleen",
            LastName = "Kennedy",
            IsProducer = true,
            BirthDate = new DateTime(1953, 6, 5),
            ProducedFilms = new List<Film>()
        };

        // Add more producers here using last names
        var rudin = new Person
        {
            FirstName = "Scott",
            LastName = "Rudin",
            IsProducer = true,
            BirthDate = new DateTime(1958, 7, 14),
            ProducedFilms = new List<Film>()
        };

        var pascal = new Person
        {
            FirstName = "Amy",
            LastName = "Pascal",
            IsProducer = true,
            BirthDate = new DateTime(1958, 3, 25),
            ProducedFilms = new List<Film>()
        };

        var blum = new Person
        {
            FirstName = "Jason",
            LastName = "Blum",
            IsProducer = true,
            BirthDate = new DateTime(1969, 2, 20),
            ProducedFilms = new List<Film>()
        };

        var thomas = new Person
        {
            FirstName = "Emma",
            LastName = "Thomas",
            IsProducer = true,
            BirthDate = new DateTime(1971, 12, 9),
            ProducedFilms = new List<Film>()
        };

        var horowitz = new Person
        {
            FirstName = "Jordan",
            LastName = "Horowitz",
            IsProducer = true,
            BirthDate = new DateTime(1980, 10, 10),
            ProducedFilms = new List<Film>()
        };

        var broccoli = new Person
        {
            FirstName = "Barbara",
            LastName = "Broccoli",
            IsProducer = true,
            BirthDate = new DateTime(1960, 6, 18),
            ProducedFilms = new List<Film>()
        };

        // Adding producer named entities to the context
        context.Persons.AddRange(feige, kennedy, rudin, pascal, blum, thomas, horowitz, broccoli);


        // Adding actor-producers
        var cooper = new Person
        {
            FirstName = "Bradley",
            LastName = "Cooper",
            IsActor = true,
            IsProducer = true,
            BirthDate = new DateTime(1975, 1, 5),
            ActedInFilms = new List<Film>(),
            ProducedFilms = new List<Film>()
        };

        var witherspoon = new Person
        {
            FirstName = "Reese",
            LastName = "Witherspoon",
            IsActor = true,
            IsProducer = true,
            BirthDate = new DateTime(1976, 3, 22),
            ActedInFilms = new List<Film>(),
            ProducedFilms = new List<Film>()
        };

        var gosling = new Person
        {
            FirstName = "Ryan",
            LastName = "Gosling",
            IsActor = true,
            IsProducer = true,
            BirthDate = new DateTime(1980, 11, 12),
            ActedInFilms = new List<Film>(),
            ProducedFilms = new List<Film>()
        };

        // Adding actor-producer named entities to the context
        context.Persons.AddRange(cooper, witherspoon, gosling);


        // Adding actor-directors
        var eastwood = new Person
        {
            FirstName = "Clint",
            LastName = "Eastwood",
            IsActor = true,
            IsDirector = true,
            BirthDate = new DateTime(1930, 5, 31),
            ActedInFilms = new List<Film>(),
            DirectedFilms = new List<Film>()
        };

        var affleck = new Person
        {
            FirstName = "Ben",
            LastName = "Affleck",
            IsActor = true,
            IsDirector = true,
            BirthDate = new DateTime(1972, 8, 15),
            ActedInFilms = new List<Film>(),
            DirectedFilms = new List<Film>()
        };

        var waititi = new Person
        {
            FirstName = "Taika",
            LastName = "Waititi",
            IsActor = true,
            IsDirector = true,
            BirthDate = new DateTime(1975, 8, 16),
            ActedInFilms = new List<Film>(),
            DirectedFilms = new List<Film>()
        };

        // Adding actor-director named entities to the context
        context.Persons.AddRange(eastwood, affleck, waititi);

        // Adding director-producers
        var clooney = new Person
        {
            FirstName = "George",
            LastName = "Clooney",
            IsDirector = true,
            IsProducer = true,
            BirthDate = new DateTime(1961, 5, 6),
            DirectedFilms = new List<Film>(),
            ProducedFilms = new List<Film>()
        };

        var coppola = new Person
        {
            FirstName = "Sofia",
            LastName = "Coppola",
            IsDirector = true,
            IsProducer = true,
            BirthDate = new DateTime(1971, 5, 14),
            DirectedFilms = new List<Film>(),
            ProducedFilms = new List<Film>()
        };

        var coogler = new Person
        {
            FirstName = "Ryan",
            LastName = "Coogler",
            IsDirector = true,
            IsProducer = true,
            BirthDate = new DateTime(1986, 5, 23),
            DirectedFilms = new List<Film>(),
            ProducedFilms = new List<Film>()
        };

        // Create the Person entities
        Person darabont = new Person
        {
            FirstName = "Frank",
            LastName = "Darabont",
            IsDirector = true,
            DirectedFilms = new List<Film>()
        };

        Person robbins = new Person
        {
            FirstName = "Tim",
            LastName = "Robbins",
            IsActor = true,
            ActedInFilms = new List<Film>(),
            IsProducer = true,
            ProducedFilms = new List<Film>()
        };

        Person freeman = new Person
        {
            FirstName = "Morgan",
            LastName = "Freeman",
            IsActor = true,
            ActedInFilms = new List<Film>()
        };

        Person puzo = new Person
        {
            LastName = "Puzo",
            IsProducer = true,
            ProducedFilms = new List<Film>()
        };

        Person brando = new Person
        {
            FirstName = "Marlon",
            LastName = "Brando",
            IsActor = true,
            ActedInFilms = new List<Film>()
        };

        Person pacino = new Person
        {
            FirstName = "Al",
            LastName = "Pacino",
            IsActor = true,
            ActedInFilms = new List<Film>()
        };

        Person caan = new Person
        {
            FirstName = "James",
            LastName = "Caan",
            IsActor = true,
            ActedInFilms = new List<Film>()
        };

        Person bale = new Person
        {
            FirstName = "Christian",
            LastName = "Bale",
            IsActor = true,
            ActedInFilms = new List<Film>()
        };

        Person ledger = new Person
        {
            FirstName = "Heath",
            LastName = "Ledger",
            IsActor = true,
            ActedInFilms = new List<Film>()
        };

        Person eckhart = new Person
        {
            FirstName = "Aaron",
            LastName = "Eckhart",
            IsActor = true,
            ActedInFilms = new List<Film>()
        };

        // Adding director-producer named entities to the context
        context.Persons.AddRange(clooney, coppola, coogler);

        // Save the changes
        context.SaveChanges();

        // Films
        context.Films.AddRange(
            new Film
            {
                Title = "The Shawshank Redemption",
                Description = "Directed by Francis Ford Coppola, \"The Godfather\" tells the epic story of the Corleone crime family. As patriarch Vito Corleone passes down his power to his reluctant son Michael, the film explores themes of loyalty, honor, and the corrupting influence of power. Set against the backdrop of organized crime in post-war America, \"The Godfather\" showcases the family's struggles to maintain control while navigating a treacherous underworld filled with rival families and internal conflicts.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg",
                Genres = new List<Genre> { dramaGenre },
                Tags = new List<Tag> { popularTag },
                Directors = new List<Person> { darabont },
                Producers = new List<Person> { darabont, robbins },
                Actors = new List<Person> { robbins, freeman },
                ReleaseDate = new DateTime(1994, 9, 23),
                DurationInMinutes = 142
            },
            new Film
            {
                Title = "The Godfather",
                Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                Genres = new List<Genre> { crimeGenre, dramaGenre },
                Tags = new List<Tag> { classicTag, popularTag },
                Directors = new List<Person> { coppola },
                Producers = new List<Person> { coppola, puzo },
                Actors = new List<Person> { brando, pacino, caan },
                ReleaseDate = new DateTime(1972, 3, 24),
                DurationInMinutes = 175
            },
            new Film
            {
                Title = "The Dark Knight",
                Description = "In this Batman film, Gotham City faces a new and formidable threat in the form of the Joker, a psychopathic criminal mastermind. Batman, aided by Lieutenant James Gordon and District Attorney Harvey Dent, strives to bring order to the city and thwart the Joker's plans. As the Joker unleashes chaos and tests the limits of Gotham's citizens and its heroes, Batman must confront his own inner demons and make difficult choices to protect the city. The film explores themes of heroism, sacrifice, and the blurred line between good and evil. With its intense action sequences, thought-provoking moral dilemmas, and standout performances, \"The Dark Knight\" is hailed as a groundbreaking superhero film.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg",
                Genres = new List<Genre> { actionGenre, crimeGenre, dramaGenre },
                Tags = new List<Tag> { superheroTag, popularTag },
                Directors = new List<Person> { nolan },
                Producers = new List<Person> { nolan, feige },
                Actors = new List<Person> { bale, ledger, eckhart },
                ReleaseDate = new DateTime(2008, 7, 18),
                DurationInMinutes = 152
            },
            // Add 22 more films here
            new Film
            {
                Title = "Inception",
                Description = "Dom Cobb, a skilled thief, possesses the rare ability to enter people's dreams and extract valuable information. When offered a chance at redemption, Cobb assembles a team to perform an inception—an act of planting an idea into someone's subconscious mind. As they delve into layers of dreams within dreams, the line between reality and illusion blurs, and Cobb confronts his own haunting past in a high-stakes mission that challenges the boundaries of the mind.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg",
                Genres = new List<Genre> { actionGenre, adventureGenre, scienceFictionGenre },
                Tags = new List<Tag> { mindBendingTag, popularTag },
                Directors = new List<Person> { nolan },
                Producers = new List<Person> { nolan, thomas },
                Actors = new List<Person> { dicaprio, eckhart, hathaway },
                ReleaseDate = new DateTime(2010, 7, 16),
                DurationInMinutes = 148
            },
            new Film
            {
                Title = "Pulp Fiction",
                Description = "Directed by Quentin Tarantino, \"Pulp Fiction\" weaves together multiple interconnected stories involving mobsters, hitmen, and other colorful characters in Los Angeles. The film follows the nonlinear narrative style and explores themes of crime, redemption, and the unpredictability of fate. From a pair of hitmen, Vincent Vega and Jules Winnfield, to a boxer named Butch Coolidge, a gangster's wife, and a mysterious briefcase, their lives intertwine in unexpected ways. With its snappy dialogue, memorable characters, and Tarantino's signature style, \"Pulp Fiction\" is a cult classic that defies genre conventions.",
                PosterUrl = "https://www.miramax.com/assets/Pulp-Fiction1.png",
                Genres = new List<Genre> { crimeGenre, dramaGenre },
                Tags = new List<Tag> { cultClassicTag, popularTag },
                Directors = new List<Person> { tarantino },
                Producers = new List<Person> { tarantino, clooney },
                Actors = new List<Person> { waititi, clooney, johnson },
                ReleaseDate = new DateTime(1994, 10, 14),
                DurationInMinutes = 154
            },
            new Film
            {
                Title = "Interstellar",
                Description = "In a future where Earth is facing an environmental crisis, a group of astronauts embarks on a perilous mission through a wormhole to find a new habitable planet. Led by Cooper, they leave their families behind and face the unknown dangers of deep space. As they grapple with the complexities of time dilation and the limits of human understanding, \"Interstellar\" explores themes of love, sacrifice, and the unbreakable bond between a parent and child.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg",
                Genres = new List<Genre> { adventureGenre, dramaGenre, scienceFictionGenre },
                Tags = new List<Tag> { spaceTravelTag, popularTag },
                Directors = new List<Person> { nolan },
                Producers = new List<Person> { nolan, thomas },
                Actors = new List<Person> { waititi, hathaway, johnson },
                ReleaseDate = new DateTime(2014, 11, 7),
                DurationInMinutes = 169
            },
            new Film
            {
                Title = "The Matrix",
                Description = "\"The Matrix\" (1999): In a dystopian future, \"The Matrix\" introduces us to Thomas Anderson, a computer programmer by day and a hacker named Neo by night. Neo discovers the truth about the world he lives in—a simulated reality called the Matrix created by machines to subdue humanity. With the guidance of a group of rebels led by Morpheus, Neo learns to harness his latent abilities and fights against the machines to free humanity from their control. Blending action, philosophy, and groundbreaking special effects, \"The Matrix\" challenges the nature of reality and delves into themes of identity, free will, and the struggle against oppressive systems.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                Genres = new List<Genre> { actionGenre, scienceFictionGenre },
                Tags = new List<Tag> { mindBendingTag, cultClassicTag },
                Directors = new List<Person> { scorsese },
                Producers = new List<Person> { coppola, scorsese },
                Actors = new List<Person> { johansson, smith, johnson },
                ReleaseDate = new DateTime(1999, 3, 31),
                DurationInMinutes = 136
            },
            new Film
            {
                Title = "Forrest Gump",
                Description = "\"Forrest Gump\" follows the life of a simple-minded yet kind-hearted man named Forrest Gump. Despite his low IQ, Forrest experiences a series of extraordinary events that intersect with significant moments in American history. From his childhood friendship with Jenny to his time in the Vietnam War, his success in business, and his unrequited love for Jenny, Forrest's life is a remarkable journey filled with unexpected twists and turns. Through it all, Forrest's innocence, sincerity, and unwavering belief in doing what is right make a lasting impact on those around him. The film explores themes of destiny, love, and the resilience of the human spirit, capturing the essence of the American experience.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_.jpg",
                Genres = new List<Genre> { dramaGenre, romanceGenre },
                Tags = new List<Tag> { heartwarmingTag, popularTag },
                Directors = new List<Person> { scorsese },
                Producers = new List<Person> { coppola },
                Actors = new List<Person> { hanks, johnson, waititi },
                ReleaseDate = new DateTime(1994, 7, 6),
                DurationInMinutes = 142
            },
            new Film
            {
                Title = "The Silence of the Lambs",
                Description = "FBI trainee Clarice Starling is assigned to seek the help of the incarcerated cannibalistic serial killer, Dr. Hannibal Lecter, in order to catch another serial killer known as Buffalo Bill. As Clarice delves deeper into the twisted minds of these criminals, she must confront her own fears and navigate a deadly game of cat and mouse. With its psychological intensity, memorable performances by Jodie Foster and Anthony Hopkins, and a chilling narrative, \"The Silence of the Lambs\" is a gripping thriller that has become a cinematic classic.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BNjNhZTk0ZmEtNjJhMi00YzFlLWE1MmEtYzM1M2ZmMGMwMTU4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                Genres = new List<Genre> { crimeGenre, dramaGenre, thrillerGenre },
                Tags = new List<Tag> { psychologicalThrillerTag, cultClassicTag },
                Directors = new List<Person> { gerwig },
                Producers = new List<Person> { thomas, coppola },
                Actors = new List<Person> { smith, waititi, johansson },
                ReleaseDate = new DateTime(1991, 2, 14),
                DurationInMinutes = 118
            },
            new Film
            {
                Title = "Inglourious Basterds",
                Description = "Set during World War II, this Quentin Tarantino film follows a group of Jewish-American soldiers known as the Basterds, led by Lieutenant Aldo Raine. They join forces with a German actress-turned-spy to execute a plan to assassinate high-ranking Nazi officials, including the ruthless Colonel Hans Landa. Filled with tension, dark humor, and Tarantino's signature dialogue, \"Inglourious Basterds\" presents an alternate history with a blend of fiction and real-life events.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BOTJiNDEzOWYtMTVjOC00ZjlmLWE0NGMtZmE1OWVmZDQ2OWJhXkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_FMjpg_UX1000_.jpg",
                Genres = new List<Genre> { adventureGenre, dramaGenre, warGenre },
                Tags = new List<Tag> { alternateHistoryTag, cultClassicTag },
                Directors = new List<Person> { tarantino },
                Producers = new List<Person> { tarantino, coppola },
                Actors = new List<Person> { pitt, horowitz, waititi },
                ReleaseDate = new DateTime(2009, 5, 20),
                DurationInMinutes = 153
            },
            new Film
            {
                Title = "The Avengers",
                Description = "Earth's mightiest heroes, including Iron Man, Captain America, Thor, Hulk, Black Widow, and Hawkeye, unite under the leadership of Nick Fury to battle against Loki, a Norse god intent on ruling the world. As the Avengers face internal conflicts and external threats, they must learn to work together and harness their unique powers to save humanity. With its high-octane action, humor, and a dynamic ensemble cast, \"The Avengers\" set the stage for the Marvel Cinematic Universe and became a global phenomenon.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg",
                Genres = new List<Genre> { actionGenre, adventureGenre, scienceFictionGenre },
                Tags = new List<Tag> { superheroTag, popularTag },
                Directors = new List<Person> { gerwig, nolan },
                Producers = new List<Person> { feige },
                Actors = new List<Person> { downey, evans, smith },
                ReleaseDate = new DateTime(2012, 5, 4),
                DurationInMinutes = 143
            },
            new Film
            {
                Title = "Gone Girl",
                Description = "With his wife's disappearance having become the focus of an intense media circus, a man sees the spotlight turned on him when it's suspected that he may not be innocent.",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTk0MDQ3MzAzOV5BMl5BanBnXkFtZTgwNzU1NzE3MjE@._V1_FMjpg_UX1000_.jpg",
                Genres = new List<Genre> { crimeGenre, dramaGenre, mysteryGenre },
                Tags = new List<Tag> { psychologicalThrillerTag, suspensefulTag },
                Directors = new List<Person> { gerwig },
                Producers = new List<Person> { feige, coppola },
                Actors = new List<Person> { affleck, johnson, hanks },
                ReleaseDate = new DateTime(2014, 10, 3),
                DurationInMinutes = 149
            }
        );

        var lionKing = new Film
        {
            Title = "The Lion King",
            Description = "Set in the Pride Lands of Africa, \"The Lion King\" tells the timeless coming-of-age story of Simba, a young lion prince destined to succeed his father, Mufasa, as king. However, Simba's uncle, Scar, plots to seize the throne for himself, leading to a chain of events that forces Simba into exile. With the help of newfound friends, Timon and Pumbaa, Simba embarks on a journey of self-discovery and learns to embrace his destiny. Combining breathtaking animation, memorable songs, and heartfelt storytelling, \"The Lion King\" explores themes of identity, loss, and the circle of life, leaving audiences both enchanted and moved by its powerful tale of courage and redemption.",
            PosterUrl = "https://m.media-amazon.com/images/M/MV5BYTYxNGMyZTYtMjE3MS00MzNjLWFjNmYtMDk3N2FmM2JiM2M1XkEyXkFqcGdeQXVyNjY5NDU4NzI@._V1_.jpg",
            Genres = new List<Genre> { adventureGenre, animationGenre, dramaGenre },
            Tags = new List<Tag> { familyFriendlyTag, classicTag },
            Directors = new List<Person> { waititi, gerwig },
            Producers = new List<Person> { thomas, coppola },
            Actors = new List<Person> { stone, hathaway, jackman },
            ReleaseDate = new DateTime(1994, 6, 15),
            DurationInMinutes = 88
        };
        var theSocialNetwork = new Film
        {
            Title = "The Social Network",
            Description = "Directed by David Fincher, \"The Social Network\" chronicles the rise of Facebook and the complex relationships behind its creation. Mark Zuckerberg, a brilliant but socially challenged Harvard student, launches the social networking site with his friend Eduardo Saverin. However, as Facebook's success grows, so does the tension and legal disputes among the founders. Through its sharp dialogue, nuanced performances, and exploration of ambition and betrayal, \"The Social Network\" offers a compelling look at the genesis of a global phenomenon.",
            PosterUrl = "https://pics.filmaffinity.com/The_Social_Network-460155430-large.jpg",
            Genres = new List<Genre> { biographyGenre, dramaGenre },
            Tags = new List<Tag> { trueStoryTag, thoughtProvokingTag },
            Directors = new List<Person> { waititi },
            Producers = new List<Person> { feige },
            Actors = new List<Person> { smith, johnson, hathaway },
            ReleaseDate = new DateTime(2010, 10, 1),
            DurationInMinutes = 120
        };
        var laLaLand = new Film
        {
            Title = "La La Land",
            Description = "Set in modern-day Los Angeles, \"La La Land\" follows the romantic journey of Mia, an aspiring actress, and Sebastian, a jazz pianist. As they pursue their dreams, their paths intertwine and they fall in love, but their ambitions put their relationship to the test. Through dazzling musical numbers, vibrant visuals, and a bittersweet narrative, \"La La Land\" captures the magic and challenges of following one's passions in a city known for its dreams.",
            PosterUrl = "https://m.media-amazon.com/images/M/MV5BMzUzNDM2NzM2MV5BMl5BanBnXkFtZTgwNTM3NTg4OTE@._V1_.jpg",
            Genres = new List<Genre> { dramaGenre, musicalGenre, romanceGenre },
            Tags = new List<Tag> { musicTag, romanticTag },
            Directors = new List<Person> { waititi },
            Producers = new List<Person> { thomas, },
            Actors = new List<Person> { gosling, stone },
            ReleaseDate = new DateTime(2016, 12, 9),
            DurationInMinutes = 128
        };

        context.Films.AddRange(lionKing, theSocialNetwork, laLaLand);

        await context.SaveChangesAsync();

        // --
        // Reviews

        Random random = new Random();
        string[] reviewCommets = {
    "Amazing film! The storyline was captivating, and the performances were outstanding.",
    "I couldn't take my eyes off the screen. The visuals were stunning, and the acting was top-notch.",
    "This film is a masterpiece. It kept me on the edge of my seat from start to finish.",
    "I was blown away by the depth of the characters and the thought-provoking themes explored in this film.",
    "A must-watch! The director did a phenomenal job bringing this story to life.",
    "I can't recommend this film enough. It's a true cinematic experience that will stay with you long after it ends."
};

        var reviews = new List<Review>();
        foreach (var film in context.Films)
        {
            for (int i = 0; i < 7; i++)
            {

                var user = users[i+4];
                var reviewComment = reviewCommets[random.Next(reviewCommets.Length)];

                var filmReview = new Review
                {
                    FilmId = film.Id,
                    UserId = user.Id,
                    Rating = random.Next(4, 11),
                    Comment = reviewComment,
                    CreatedAt = DateTime.Now.AddDays(-random.Next(1, 365))
                };

                reviews.Add(filmReview);
            }
        }

        foreach (var review in reviews)
        {
            review.DomainEvents.Add(new ReviewUpsertedEvent(review));
        }

        context.Reviews.AddRange(reviews);

        await context.SaveChangesAsync();
        // --


        // --
        // Watched
        regularUser1.AuthenticationResult.User.Watched.Films.Add(lionKing);
        regularUser1.AuthenticationResult.User.Watched.Films.Add(theSocialNetwork);

        regularUser2.AuthenticationResult.User.Watched.Films.Add(laLaLand);

        await context.SaveChangesAsync();
        // --

        // --
        // WatchLater
        regularUser1.AuthenticationResult.User.WatchLater.Films.Add(laLaLand);

        regularUser2.AuthenticationResult.User.WatchLater.Films.Add(lionKing);
        regularUser2.AuthenticationResult.User.WatchLater.Films.Add(theSocialNetwork);

        await context.SaveChangesAsync();
        // --

    }
}
