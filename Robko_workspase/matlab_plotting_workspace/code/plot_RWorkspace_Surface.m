%This code plotting Robko 01 workspace by Surface%

%Run this file from Matlab (drug and drop in Matlab command window)
%or load this file folder in Matlab and run this code in Matlab (copypast)

[x,y,z] = getWorkspacePoints(30);
k = boundary(x,y,z);
figure('Name','Surface Robko 01 workspace Plot','NumberTitle','off');
for i = 1:4
    subplot(2,2,i)
    trisurf(k,x,y,z,'Facecolor','red','FaceAlpha',0.1, 'EdgeAlpha', 0.2);
    if Constants.DO_CHESS_MWORKSPACE doChessManipulateWorkspace();end
    setView(i);
end
%print(gcf, '-dpdf', 'RWorkspace_Surface.pdf', '-bestfit');
print(gcf, '-djpeg', '..\result\RWorkspace_Surface.jpeg');



