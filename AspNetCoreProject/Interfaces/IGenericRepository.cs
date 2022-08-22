﻿using System.Collections.Generic;

namespace AspNetCoreProject.Interfaces
{
    public interface IGenericRepository<Tablo> where Tablo : class , new()
    {
        void Ekle(Tablo tablo);
        void Guncelle(Tablo tablo);
        void Sil(Tablo tablo);
        List<Tablo> GetirHepsi();
        Tablo GetirId(int Id);

    }
}
