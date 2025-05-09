using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of the MusicPlayer class
            MusicPlayer player = new MusicPlayer();
            // Add some songs to the playlist
            player.AddSong("Song 1");
            player.AddSong("Song 2");
            player.AddSong("Song 3");
            // Play the first song
            player.Play(0);
            // Pause the song
            player.Pause();
            // Resume playing the song
            player.Resume();
            // Stop playing the song
            player.Stop();
            // Remove a song from the playlist
            player.RemoveSong(1);
            // Display the remaining songs in the playlist
            player.DisplayPlaylist();
        }
    }
}
