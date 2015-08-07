XBUILD := xbuild

XBUILDFLAGS := /p:TargetFrameworkProfile=''
XBUILDFLAGS_RELEASE := $(XBUILDFLAGS) /p:Configuration=Release

SOLUTION := Game/Game.sln

deploy: cleanrunner
	$(XBUILD) $(XBUILDFLAGS_RELEASE) $(SOLUTION)
	cp -f play_icfp2015.base play_icfp2015
	chmod +rx play_icfp2015

cleanrunner:
	rm -f play_icfp2015

clean: cleanrunner
	$(XBUILD) $(XBUILDFLAGS_RELEASE) $(SOLUTION) /t:Clean

.SUFFIXES:
.PHONY: deploy release  clean