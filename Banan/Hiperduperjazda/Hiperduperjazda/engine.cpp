#include "engine.h"

bool AppInit()
{
	srand(time(NULL));

	if(SDL_Init(SDL_INIT_EVERYTHING) == -1)
	{
		return false;
	}

	screen = SDL_SetVideoMode(SCREEN_WIDTH, SCREEN_HEIGHT, SCREEN_BPP, SDL_HWSURFACE | SDL_DOUBLEBUF);

	if(screen == NULL)
	{
		return false;
	}

	if(TTF_Init() == -1)
	{
		return false;
	}

	SDL_WM_SetCaption("BANAN AKCJA", NULL);

	return true;
}

bool load_files(std::string file, SDL_Surface* surfacePtr)
{
	surfacePtr = load_image(file);
	if(surfacePtr == NULL)
	{
		return false;
	}

	return true;
}

int apply_surface( int x, int y, SDL_Surface* source, SDL_Surface* destination, SDL_Rect* clip)
{
	SDL_Rect offset;

	offset.x = x;
	offset.y = y;

	return SDL_BlitSurface(source, clip, destination, &offset);
}

SDL_Surface *load_image(std::string filename)
{
	SDL_Surface* loadedImage = NULL;
	SDL_Surface* optimizedImage = NULL;

	loadedImage = IMG_Load(filename.c_str());

	if(loadedImage != NULL)
	{
		optimizedImage = SDL_DisplayFormat(loadedImage);
		SDL_FreeSurface(loadedImage);
		if(optimizedImage != NULL)
		{
			Uint32 colorkey = SDL_MapRGB(optimizedImage->format, 0xFF, 0, 0xFF); // magenta alpha
			SDL_SetColorKey(optimizedImage, SDL_SRCCOLORKEY, colorkey);
		}
	}
	return optimizedImage;
}

void clean_up()
{
	SDL_FreeSurface(background);
	SDL_FreeSurface(message);
	TTF_Quit();
	SDL_Quit();
}

void framerate::Init(float tfps)
{
	targetfps = tfps;
	frameDelay = SDL_GetTicks();

}

void framerate::SetSpeedFactor()
{
	currentTicks = SDL_GetTicks();
	speedFactor = (float)(currentTicks - frameDelay)/((float)1000.f/targetfps);
	fps = targetfps/speedFactor;
	/*if(speedFactor <= 0)
		speedFactor = 1;*/
	frameDelay = currentTicks;
}

int GetRandomNumber(int range, int offset)
{
	return rand() % range + offset;
}


int GetRandomColor(SDL_Surface* format)
{
	return SDL_MapRGB(format->format,GetRandomNumber(256), GetRandomNumber(256), GetRandomNumber(256));
}