﻿namespace photuris_backend.DTOs
{
    public class ChangePasswordDto
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? NewPasswordConfirmation { get; set; }
    }
}
