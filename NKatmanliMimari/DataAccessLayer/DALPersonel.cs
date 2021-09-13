using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALPersonel
    {
        public static List<EntityPersonel> PersonelListesi()
        {
            List<EntityPersonel> degerler = new List<EntityPersonel>();
            SqlCommand komut1 = new SqlCommand("select * from Bilgi", Baglanti.bgl);

            if (komut1.Connection.State != ConnectionState.Open)
            {
                komut1.Connection.Open();
            }
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                EntityPersonel ent = new EntityPersonel();
                ent.Id = int.Parse(dr["pId"].ToString());
                ent.Ad = dr["pAd"].ToString();
                ent.Soyad = dr["pSoyad"].ToString();
                ent.Sehir = dr["pSehir"].ToString();
                ent.Görev = dr["pGorev"].ToString();
                ent.Maas = short.Parse(dr["pMaas"].ToString());

                degerler.Add(ent);
            }
            dr.Close();
            return degerler;
        }

        public static int PersonelEkle(EntityPersonel p)
        {
            SqlCommand komut = new SqlCommand("insert into Bilgi (pAd, pSoyad,pGorev,pSehir,pMaas) values(@p1,@p2,@p3,@p4,@p5)", Baglanti.bgl);
            if (komut.Connection.State != ConnectionState.Open)
            {
                komut.Connection.Open();
            }
            komut.Parameters.AddWithValue("@p1", p.Ad);
            komut.Parameters.AddWithValue("@p2", p.Soyad);
            komut.Parameters.AddWithValue("@p3", p.Görev);
            komut.Parameters.AddWithValue("@p4", p.Sehir);
            komut.Parameters.AddWithValue("@p5", p.Maas);

            return komut.ExecuteNonQuery();
        }

        public static bool PersonelSil(int p)
        {
            SqlCommand komut = new SqlCommand("delete from Bilgi where pID=@p1", Baglanti.bgl);
            if (komut.Connection.State != ConnectionState.Open)
            {
                komut.Connection.Open();
            }

            komut.Parameters.AddWithValue("@p1", p);
            return komut.ExecuteNonQuery() > 0;
        }

        public static bool PersonelGüncelle(EntityPersonel ent)
        {
            SqlCommand komut = new SqlCommand("update Bilgi set pAd=@p1, pSoyad=@p2, pSehir=@p3, pGorev=@p4, pMaas=@p5 WHERE pId=@p6", Baglanti.bgl);
            if (komut.Connection.State != ConnectionState.Open)
            {
                komut.Connection.Open();
            }

            komut.Parameters.AddWithValue("@p1", ent.Ad);
            komut.Parameters.AddWithValue("@p2", ent.Soyad);
            komut.Parameters.AddWithValue("@p3", ent.Sehir);
            komut.Parameters.AddWithValue("@p4", ent.Görev);
            komut.Parameters.AddWithValue("@p5", ent.Maas);
            komut.Parameters.AddWithValue("@p6", ent.Id);

            return komut.ExecuteNonQuery()>0;
        }
    }
}
