#include <sstream>
#include <vector>
#include "SDL.h"
#include "SDL_image.h"
#include "SDL_ttf.h"
//my headers
#include "defines.h"
#include "engine.h"
#include "character.h"
#include "timer.h"
#include "map.h"
#include "weapon.h"

bool quit = false;
bool hud = false;

SDL_Surface* screen = NULL;
SDL_Surface* message = NULL;
SDL_Surface* background = NULL;

SDL_Event event;
framerate fps;

int mapColor;
TTF_Font *font = NULL;
SDL_Color textColor = {0,0,0};
Map mapa;
Wpn bron;

int main(int argv, char *args[])
{
	fps.Init(1);
	//delta.start();
	AppInit();

	mapColor = GetRandomColor(screen);
	mapa.GenerateMap(mapColor);
	
	Banana bananek(0,0);	
	bananek.RandomizePosition();

	bananek.LoadFiles();
	background = load_image("background.png");

	font = TTF_OpenFont( "lazy.ttf", 18 );
	
	if(font == NULL)
		return 1;

	while( quit == false )
	{
        while( SDL_PollEvent( &event ) ) // events
        {			
			bananek.handle_input();
            //If the user has Xed out the window
            if( event.type == SDL_QUIT )
            {
                //Quit the program
                quit = true;
            }
        }
		
		//logika
		bananek.Dynamic();
		fps.SetSpeedFactor();
		bananek.Move(fps.speedFactor, mapa.GetElements());
		bron.Move(fps.speedFactor);
		//delta.start();

		SDL_FillRect(screen, &screen->clip_rect, SDL_MapRGB(screen->format, 0xFF, 0xFF, 0xFF));
		apply_surface(-50-bananek.GetX()*100/SCREEN_WIDTH/5,-50-bananek.GetY()*100/SCREEN_HEIGHT/5,background,screen);

		mapa.Draw(screen);

		if(hud)
		{
			std::ostringstream temp;
			temp << "FPS: " << fps.fps;
			std::vector<SDL_Surface*> vector;
			vector.push_back(TTF_RenderText_Solid(font, temp.str().c_str(), textColor));
			apply_surface(0,0,vector.at(0),screen);
			SDL_FreeSurface(vector.at(0));
			vector.clear();
			vector = bananek.PlayerInfo();
			
			for(int i=0;i<vector.size();i++)
			{
				if(vector.at(i) != NULL)
				{
					apply_surface(0,20+i*20,vector.at(i),screen);
					SDL_FreeSurface(vector.at(i));
				}
			}
			vector.clear();
			vector = mapa.MapInfo();
			for(int i = 0; i < vector.size();i++)
			{
				if(vector.at(i) != NULL)
				{
					apply_surface(0,80 + 20*i,vector.at(i),screen);
					SDL_FreeSurface(vector.at(i));
				}
			}
		}

		//
		bananek.Show(screen);
		bron.Draw(screen);
		if( SDL_Flip( screen ) == -1 ) // render
        {
            return 1;    
        }
	}

	clean_up();

	//clean
	return 0;
}