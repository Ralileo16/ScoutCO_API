using System;
using System.Collections.Generic;

namespace ScoutCO_API.Models;

public partial class News
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ThumbnailLink { get; set; } = null!;

    public string AlbumlLink { get; set; } = null!;

    public DateOnly Date { get; set; }
}
