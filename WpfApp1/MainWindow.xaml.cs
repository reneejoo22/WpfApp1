using WpfApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHello_Click(object sender, RoutedEventArgs e)
        {
            // 버튼 클릭 시 실행할 코드
            tbHi.Text = tbSayHello.Text + " World";
        }

        // MainWindow.xaml.cs 상단
        private MyNewWindow newWindowInstance; // 기존 창을 기억

        // 버튼 클릭 이벤트
        private void btnNewWorld_Click(object sender, RoutedEventArgs e)
        {
            if (newWindowInstance == null || !newWindowInstance.IsLoaded)
            {
                newWindowInstance = new MyNewWindow();

                // 부모 창 지정
                newWindowInstance.Owner = this;

                // 위치 변경 시 체크
                newWindowInstance.LocationChanged += ChildWindow_LocationChanged;

                // 창 크기와 부모 창 안에서 시작
                newWindowInstance.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                newWindowInstance.Show();
            }
            else
            {
                // 이미 열려 있는 창이 있으면 최소화 상태 해제 후 포커스
                if (newWindowInstance.WindowState == WindowState.Minimized)
                {
                    newWindowInstance.WindowState = WindowState.Normal;
                    newWindowInstance.Activate(); // 창을 최상위로
                }
                else
                {
                    newWindowInstance.WindowState = WindowState.Minimized; // 최소화
                }
                
            }
        }
        private void ChildWindow_LocationChanged(object sender, EventArgs e)
        {
            var child = sender as Window;
            if (child == null) return;

            // 부모 창 위치/크기
            var parentLeft = this.Left;
            var parentTop = this.Top;
            var parentRight = this.Left + this.Width;
            var parentBottom = this.Top + this.Height;

            // 자식 창 위치/크기
            var childLeft = child.Left;
            var childTop = child.Top;
            var childRight = child.Left + child.Width;
            var childBottom = child.Top + child.Height;

            // 왼쪽/위쪽 제한
            if (childLeft < parentLeft) child.Left = parentLeft;
            if (childTop < parentTop) child.Top = parentTop;

            // 오른쪽/아래쪽 제한
            if (childRight > parentRight) child.Left = parentRight - child.Width;
            if (childBottom > parentBottom) child.Top = parentBottom - child.Height;
        }


    }
}
