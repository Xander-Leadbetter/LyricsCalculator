using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LyricsCalculator
{
    internal class Program
    {
        static async Task Main()
        {
            // Initialize API Client
            Api.Init_ApiClient();

            string artistInput = "";
            var validInput = false;

            while (!validInput)
            {
                Console.Write("\nPlease Enter Artists Name: ");
                artistInput = Console.ReadLine();

                validInput = ValidArtistName(artistInput);

                if (!validInput)
                {
                    Console.WriteLine("\nInput Invalid - Press return and try again");
                    Console.ReadLine();
                    Console.Clear();
                }

            }

            Artist artist = new Artist { Name = artistInput };

            // Request the MBID for the arist
            Console.Write("\rRequesting Music Brainz ID.... ");
            artist.MBID = await MusicBrainz_RequestMBID(artist.Name);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");


            // Request Album details for artist
            Console.ForegroundColor = ConsoleColor.Gray; ;
            Console.Write("\rRequesting Album and Track details.... ");
            artist.Albums = await MusicBrainz_RequestAlbums(artist);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");

            // Request Lyrics for each song, for each album
            Console.ForegroundColor = ConsoleColor.Gray; ;
            Console.Write("\rRequesting Lyrics for tracks....");
            foreach (Album album in artist.Albums)
            {
                artist.AlbumCount++;
                album.Tracks = await Lyrics_RequestLyrics(artist.Name, album.Tracks);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + artist.Name);
            // Display results
            DisplayResults(artist);


        }

        private static bool ValidArtistName(string? name)
        {
            bool valid = true;

            if (string.IsNullOrWhiteSpace(name))
            {
                valid = false;
            }

            return valid;
        }

        private static async Task<string> MusicBrainz_RequestMBID(string artist)
        {
            string mbid = "";

            // Form call

            string url = $"https://musicbrainz.org/ws/2/artist/?query={Uri.EscapeDataString(artist)}&fmt=json";

            // Call API
            try
            {
                using (HttpResponseMessage resp = await Api.ApiClient.GetAsync(url))
                {
                    try
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            // Format response to object
                            ArtistJson.ArtistData artistDetails = await resp.Content.ReadAsAsync<ArtistJson.ArtistData>();

                            if (artistDetails.Artists.Count > 0)
                            {

                                // Retrieve mbid for first band name match (could expand to selection of artists if more than one returned)
                                mbid = artistDetails.Artists.FirstOrDefault().Id;
                            }
                            else
                            {
                                Console.WriteLine("No Artist Found for: " + artist);
                            }

                        }
                        else
                        {
                            Console.WriteLine("Failure from API - Error Code:" + resp.StatusCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Issue calling WebBrainz API - Please try again");
                        Console.WriteLine(ex);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Issue instantiating API");
                Console.WriteLine(ex);
            }

            return mbid;
        }

        private static async Task<List<Album>> MusicBrainz_RequestAlbums(Artist artist)
        {

            bool offsetReached = false;
            bool error = false;
            int offset = 0;
            int attempts = 0;

            List<Album> albums = new List<Album>();
            List<Album> tempAlbums = new List<Album>();

            // Form call
            string url = $"https://musicbrainz.org/ws/2/release?artist={Uri.EscapeDataString(artist.MBID)}&inc=release-groups+recordings&status=official&type=album&fmt=json&offset=";

            // Call API
            try
            {
                while (!offsetReached && !error && attempts <= 3)
                {
                    // Adding 1 sec delay to prevent 503 - see https://musicbrainz.org/doc/MusicBrainz_API/Rate_Limiting#:~:text=The%20rate%20at%20which%20your,average)%201%20request%20per%20second. for further details
                    Thread.Sleep(1000);
                    using (HttpResponseMessage resp = await Api.ApiClient.GetAsync(url + offset.ToString()))
                    {
                        try
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                int releaseGroupCount = 0;
                                string det = await resp.Content.ReadAsStringAsync();

                                // Format response to object
                                AlbumJson.ReleaseData albumDetails = await resp.Content.ReadAsAsync<AlbumJson.ReleaseData>();

                                // Check "release-group-count" against offset, if reached break loop - else incriment offset by 25 (could increment by 100 according to the documentation but calls are limited to 25 with filters)
                                releaseGroupCount = albumDetails.ReleaseCount;
                                if (releaseGroupCount < offset)
                                {
                                    offsetReached = true;
                                }
                                else
                                {
                                    offset = offset + 25;
                                }

                                //Cycle through retrieved albums
                                foreach (var album in albumDetails.Releases)
                                {
                                    string[] secTypesFilter = { "audio drama", "audiobook", "broadcast", "compilation", "dj -mix", "interview", "live", "mixtape /street", "remix", "soundtrack", "spokenword" };

                                    // Ignore anything that is in the secondary types filter, we only want primary types of albums/ep
                                    if (!album.ReleaseGroup.SecondaryTypes.Any(x => secTypesFilter.Any(y => y.ToLower() == x.ToLower())))
                                    {
                                        List<Track> tracks = new List<Track>();

                                        foreach (var track in album.Media[0].Tracks)
                                        {
                                            tracks.Add(new Track { Title = track.Title });

                                        }

                                        // Add album and song list to return album list
                                        tempAlbums.Add(new Album { Title = album.Title, MBID = album.Id, Tracks = tracks, TrackCount = tracks.Count() });
                                    }
                                }
                            }
                            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                            {
                                Console.WriteLine("No Albums Found for: " + artist);
                            }
                            else if (resp.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                            {
                                attempts++; // increment attempt to stop potential spam against web service
                                offset = offset - 25; // Decrement offset to try again
                                Console.WriteLine("Service Unavalabile, Trying again");
                            }
                            else
                            {
                                error = true;
                                Console.WriteLine("Failure from WebBrainz API - Status Code: " + resp.StatusCode);
                            }
                        }
                        catch (Exception ex)
                        {
                            error = true;
                            Console.WriteLine("Issue calling WebBrainz API - Please try again");
                            Console.WriteLine(ex);
                        }

                    }
                }

                if (attempts > 3)
                {
                    Console.WriteLine("Issue calling WebBrainz API - Attempts Made > 3");
                }

            }
            catch (Exception ex)
            {
                error = true;
                Console.WriteLine("Issue instantiating API");
                Console.WriteLine(ex);
            }

            // Remove duplicate album names (could filter this by country but this seems better, should it not be released in the country filtered)
            List<Album> filteredAlbums = tempAlbums.GroupBy(x => x.Title.ToLower()).Select(x => x.First()).ToList();

            albums.AddRange(filteredAlbums);

            return albums.OrderBy(o => o.Title).ToList();
        }

        private static async Task<List<Track>> Lyrics_RequestLyrics(string artist, List<Track> tracks)
        {
            List<Track> tracksWithLyrics = new List<Track>();


            foreach (Track track in tracks.OrderBy(o => o.Title).ToList())
            {
                // Added delay to stop potential flagging as spam from untrusted client
                Thread.Sleep(250);

                // (it appears that URI escape chars doesn't work with this particular API in certain cases, replacing with spaces)
                //TODO: Simplify this
                string title = track.Title.Replace("&", "and").Replace('/', ' ').Replace('?', ' ').Replace(',', ' ').Replace("'", " ");

                // Form call 
                string url = $"https://api.lyrics.ovh/v1/{Uri.EscapeDataString(artist)}/{Uri.EscapeDataString(title)}";

                // Call API
                try
                {
                    using (HttpResponseMessage resp = await Api.ApiClient.GetAsync(url))
                    {
                        try
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                // Format response to object
                                LyricsJson.LyricsData lyrics = await resp.Content.ReadAsAsync<LyricsJson.LyricsData>();

                                // Remove first line of the returned lyrics as that appears to be the title and artist

                                List<string> lyricsSplit = new List<string>();

                                // Work Around for different returns for the same song is below, see README for further information under "Known Issues"
                                // If the lyrics starts with a band/title summary perform the following, else just split the string
                                if (lyrics.Lyrics.Split(new char[] { ' ' })[0] == "Paroles")
                                {
                                    // It appears \n has a space as a prefix
                                    string noFirst = lyrics.Lyrics.Replace("\r\n", "|").Split('|').Last();
                                    lyricsSplit = noFirst.Replace("\n", " ").Split().ToList().Where(x => x != "").ToList();
                                }
                                else
                                {
                                    // It appears \r has no space as a prefix
                                    lyricsSplit = lyrics.Lyrics.Replace("\r", "").Split().ToList().Where(x => x != "").ToList();
                                }

                                Track parsedTrack = new Track
                                {
                                    Title = track.Title,
                                    LyricsEnum = lyricsSplit,
                                    WordCount = lyricsSplit.Count()
                                };

                                tracksWithLyrics.Add(parsedTrack);

                            }
                            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                            {
                                Console.WriteLine("\nNo Lyrics Found for: " + artist + " - " + track.Title);
                            }
                            else
                            {
                                Console.WriteLine("Failure from lyrics.ovh API - Error Code:" + resp.StatusCode);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Issue calling lyrics.ovh API - Please try again");
                            Console.WriteLine(ex);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Issue instantiating API");
                    Console.WriteLine(ex);
                }
            }

            return tracksWithLyrics;
        }

        private static void DisplayResults(Artist artist)
        {
            int totalwords = 0;
            int numbertracks = 0;

            foreach (Album album in artist.Albums)
            {
                numbertracks = numbertracks + album.TrackCount;

                foreach (Track track in album.Tracks)
                {
                    totalwords = totalwords + track.WordCount;
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Artist MBID: " + artist.MBID);
            Console.WriteLine("Total Number of Albums: " + artist.AlbumCount);
            Console.WriteLine("Total Number of Tracks: " + numbertracks);
            Console.WriteLine("Total Number of Words: " + totalwords);

            if(totalwords == 0 || numbertracks == 0)
            {
                Console.WriteLine("Average Number of Words: 0");
            }
            else
            {
                Console.WriteLine("Average Number of Words: " + totalwords / numbertracks);
            }

            Console.WriteLine("Press return or enter to exit");
            Console.ReadLine();
            Environment.Exit(0);
        }

    }
}
