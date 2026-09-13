<<<<<<< HEAD
﻿using System.Reflection;
using Vortice.XInput;

namespace practice1
=======
﻿namespace practice1
>>>>>>> fd5e7d46f24505cfd4dba3f6bd8bc653f5619870
{
    internal class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            //Console.WriteLine(10.GetType());
            //Console.WriteLine(10.0F.GetType());
            //Console.WriteLine('한'.GetType());
            //Console.WriteLine("message".GetType());

            //var value = "안녕하세요"
            //Console.WriteLine("value type: " + value.GetType());

            Image bgImg;
            Image titleImg;
            Image startImg;

            void Initialize()
            {
                bgImg = Image.FromFile("resource/background1.png");
            }


            float titleTimer = 0f;

            void Update(float deltaTime)
            {
                titleTimer += 0.05f;
            }


            void Draw(Graphics d)
            {
                
                d.DrawImage(bgImg, 0, 0);
                float titleX = (800 / 2) - (titleImg.Width / 2);
                float titleY = 100 + (float)Math.Sin(titleTimer) * 15f;
                d.DrawImage(titleImg, titleX, titleY);

                float titleX = (800 / 2) - (titleImg.Width / 2);
                float titleY = 100 + (float)Math.Sin(titleTimer) * 15f;
                d.DrawImage(titleImg, titleX, titleY);

                
                float drawStartX = startBtnBounds.X - (startImg.Width * (startBtnScale - 1.0f) / 2);
                float drawStartY = startBtnBounds.Y - (startImg.Height * (startBtnScale - 1.0f) / 2);

                
                DrawImageScaled(startImg, drawStartX, drawStartY, startBtnScale);
            }
            float startBtnScale = 1.0f; 
            Rectangle startBtnBounds;

            void Update(float deltaTime)
            {
                titleTimer += 0.05f;

                
                int startX = (800 / 2) - (startImg.Width / 2);
                int startY = 450;

                
                startBtnBounds = new Rectangle(startX, startY, startImg.Width, startImg.Height);

               
                int mouseX = Input.MouseX;
                int mouseY = Input.MouseY;

              
                if (startBtnBounds.Contains(mouseX, mouseY))
                {
                    startBtnScale = 1.15f; 
                }
                else
                {
                    startBtnScale = 1.0f;  
                }
            }
=======
            Console.WriteLine("Hello, World!");
>>>>>>> fd5e7d46f24505cfd4dba3f6bd8bc653f5619870
        }
    }
}
