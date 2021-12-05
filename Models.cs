using Newtonsoft.Json;
using System.Collections.Generic;

namespace LyricsCalculator
{
    public class AlbumJson
    {
        public class ReleaseGroup
        {

            [JsonProperty("title")]
            public string? Title { get; set; }

            [JsonProperty("secondary-type-ids")]
            public List<string>? SecondaryTypeIds { get; set; }

            [JsonProperty("disambiguation")]
            public string? Disambiguation { get; set; }

            [JsonProperty("primary-type")]
            public string? PrimaryType { get; set; }

            [JsonProperty("first-release-date")]
            public string? FirstReleaseDate { get; set; }

            [JsonProperty("id")]
            public string? Id { get; set; }

            [JsonProperty("secondary-types")]
            public List<string>? SecondaryTypes { get; set; }

            [JsonProperty("primary-type-id")]
            public string? PrimaryTypeId { get; set; }
        }

        public class Recording
        {

            [JsonProperty("video")]
            public bool Video { get; set; }

            [JsonProperty("length")]
            public int? Length { get; set; }

            [JsonProperty("title")]
            public string? Title { get; set; }

            [JsonProperty("first-release-date")]
            public string? FirstReleaseDate { get; set; }

            [JsonProperty("disambiguation")]
            public string? Disambiguation { get; set; }

            [JsonProperty("id")]
            public string? Id { get; set; }
        }

        public class Track
        {

            [JsonProperty("position")]
            public int? Position { get; set; }

            [JsonProperty("title")]
            public string? Title { get; set; }

            [JsonProperty("length")]
            public int? Length { get; set; }

            [JsonProperty("recording")]
            public Recording? Recording { get; set; }

            [JsonProperty("number")]
            public string? Number { get; set; }

            [JsonProperty("id")]
            public string? Id { get; set; }
        }

        public class Medium
        {

            [JsonProperty("format")]
            public string? Format { get; set; }

            [JsonProperty("tracks")]
            public List<Track>? Tracks { get; set; }

            [JsonProperty("title")]
            public string? Title { get; set; }

            [JsonProperty("position")]
            public int? Position { get; set; }

            [JsonProperty("track-offset")]
            public int? TrackOffset { get; set; }

            [JsonProperty("track-count")]
            public int? TrackCount { get; set; }

            [JsonProperty("format-id")]
            public string? FormatId { get; set; }
        }

        public class TextRepresentation
        {

            [JsonProperty("script")]
            public string? Script { get; set; }

            [JsonProperty("language")]
            public string? Language { get; set; }
        }

        public class CoverArtArchive
        {

            [JsonProperty("darkened")]
            public bool Darkened { get; set; }

            [JsonProperty("front")]
            public bool Front { get; set; }

            [JsonProperty("count")]
            public int? Count { get; set; }

            [JsonProperty("back")]
            public bool Back { get; set; }

            [JsonProperty("artwork")]
            public bool Artwork { get; set; }
        }

        public class Area
        {

            [JsonProperty("disambiguation")]
            public string? Disambiguation { get; set; }

            [JsonProperty("type-id")]
            public object? TypeId { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("type")]
            public object? Type { get; set; }

            [JsonProperty("iso-3166-1-codes")]
            public List<string>? Iso31661Codes { get; set; }

            [JsonProperty("id")]
            public string? Id { get; set; }

            [JsonProperty("sort-name")]
            public string? SortName { get; set; }
        }

        public class ReleaseEvent
        {

            [JsonProperty("area")]
            public Area? Area { get; set; }

            [JsonProperty("date")]
            public string? Date { get; set; }
        }

        public class Release
        {

            [JsonProperty("release-group")]
            public ReleaseGroup? ReleaseGroup { get; set; }

            [JsonProperty("date")]
            public string? Date { get; set; }

            [JsonProperty("media")]
            public List<Medium>? Media { get; set; }

            [JsonProperty("title")]
            public string? Title { get; set; }

            [JsonProperty("text-representation")]
            public TextRepresentation? TextRepresentation { get; set; }

            [JsonProperty("id")]
            public string? Id { get; set; }

            [JsonProperty("cover-art-archive")]
            public CoverArtArchive? CoverArtArchive { get; set; }

            [JsonProperty("barcode")]
            public string? Barcode { get; set; }

            [JsonProperty("quality")]
            public string? Quality { get; set; }

            [JsonProperty("release-events")]
            public List<ReleaseEvent>? ReleaseEvents { get; set; }

            [JsonProperty("status-id")]
            public string? StatusId { get; set; }

            [JsonProperty("packaging")]
            public string? Packaging { get; set; }

            [JsonProperty("country")]
            public string? Country { get; set; }

            [JsonProperty("asin")]
            public object? Asin { get; set; }

            [JsonProperty("disambiguation")]
            public string? Disambiguation { get; set; }

            [JsonProperty("status")]
            public string? Status { get; set; }

            [JsonProperty("packaging-id")]
            public string? PackagingId { get; set; }
        }

        public class ReleaseData
        {

            [JsonProperty("release-count")]
            public int ReleaseCount { get; set; }

            [JsonProperty("releases")]
            public List<Release>? Releases { get; set; }

            [JsonProperty("release-offset")]
            public int? ReleaseOffset { get; set; }
        }
    }

    public class ArtistJson
    {
        public class LifeSpan
        {

            [JsonProperty("ended")]
            public object? Ended { get; set; }
        }

        public class Area
        {

            [JsonProperty("id")]
            public string? Id { get; set; }

            [JsonProperty("type")]
            public string? Type { get; set; }

            [JsonProperty("type-id")]
            public string? TypeId { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("sort-name")]
            public string? SortName { get; set; }

            [JsonProperty("life-span")]
            public LifeSpan? LifeSpan { get; set; }
        }

        public class BeginArea
        {

            [JsonProperty("id")]
            public string? Id { get; set; }

            [JsonProperty("type")]
            public string? Type { get; set; }

            [JsonProperty("type-id")]
            public string? TypeId { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("sort-name")]
            public string? SortName { get; set; }

            [JsonProperty("life-span")]
            public LifeSpan? LifeSpan { get; set; }
        }

        public class Alias
        {

            [JsonProperty("sort-name")]
            public string? SortName { get; set; }

            [JsonProperty("type-id")]
            public string? TypeId { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("locale")]
            public string? Locale { get; set; }

            [JsonProperty("type")]
            public string? Type { get; set; }

            [JsonProperty("primary")]
            public bool? Primary { get; set; }

            [JsonProperty("begin-date")]
            public object? BeginDate { get; set; }

            [JsonProperty("end-date")]
            public object? EndDate { get; set; }
        }

        public class Tag
        {

            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }
        }

        public class Artist
        {

            [JsonProperty("id")]
            public string? Id { get; set; }

            [JsonProperty("type")]
            public string? Type { get; set; }

            [JsonProperty("type-id")]
            public string? TypeId { get; set; }

            [JsonProperty("score")]
            public int Score { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("sort-name")]
            public string? SortName { get; set; }

            [JsonProperty("country")]
            public string? Country { get; set; }

            [JsonProperty("area")]
            public Area? Area { get; set; }

            [JsonProperty("begin-area")]
            public BeginArea? BeginArea { get; set; }

            [JsonProperty("isnis")]
            public List<string>? Isnis { get; set; }

            [JsonProperty("life-span")]
            public LifeSpan? LifeSpan { get; set; }

            [JsonProperty("aliases")]
            public List<Alias>? Aliases { get; set; }

            [JsonProperty("tags")]
            public List<Tag>? Tags { get; set; }

            [JsonProperty("disambiguation")]
            public string? Disambiguation { get; set; }

            [JsonProperty("gender-id")]
            public string? GenderId { get; set; }

            [JsonProperty("gender")]
            public string? Gender { get; set; }

            [JsonProperty("ipis")]
            public List<string>? Ipis { get; set; }
        }

        public class ArtistData
        {

            [JsonProperty("created")]
            public System.DateTime Created { get; set; }

            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("offset")]
            public int Offset { get; set; }

            [JsonProperty("artists")]
            public List<Artist>? Artists { get; set; }
        }
    }

    public class LyricsJson
    {
        public class LyricsData
        {

            [JsonProperty("lyrics")]
            public string? Lyrics { get; set; }
        }
    }

    public class Artist
    {
        public string? Name { get; set; }
        public string? MBID { get; set; }
        public List<Album> Albums { get; set; } = new List<Album>();
        public int AlbumCount { get; set; }
    }

    public class Album
    {
        public string? Title { get; set; }
        public string? MBID { get; set; }
        public List<Track> Tracks { get; set; } = new List<Track>();
        public int TrackCount { get; set; }
    }

    public class Track
    {
        public string? Title { get; set; }
        public List<string>? LyricsEnum { get; set; }
        public int WordCount { get; set; }
    }


}