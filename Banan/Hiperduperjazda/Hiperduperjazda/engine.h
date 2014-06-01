#ifndef ENGINE_H_
#define ENGINE_H_

#include "SDL.h"
#include "SDL_image.h"
#include "SDL_ttf.h"
#include "defines.h"
#include "timer.h"
#include <string>
#include <random>
#include <ctime>

extern SDL_Surface* screen;
extern SDL_Surface* message;
extern SDL_Surface* background;

bool AppInit();
int apply_surface( int x, int y, SDL_Surface* source, SDL_Surface* destination, SDL_Rect* clip = NULL); 
SDL_Surface *load_image(std::string filename);
void clean_up();
bool load_files(std::string file, SDL_Surface* surfacePtr);
int GetRandomNumber(int range, int offset = 0);
int GetRandomColor(SDL_Surface* format);

class framerate
{
public:
	float	targetfps;
	float	fps;
	LARGE_INTEGER ticksPerSecond;
	LARGE_INTEGER currentTicks;
	LARGE_INTEGER frameDelay;

	float speedFactor;

	void Init(float tfps);
	void SetSpeedFactor();
};


#endif